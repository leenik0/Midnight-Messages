using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


// handles parsing the text for each of the messages and handling which messages get sent in which order.
public class Storyboard : MonoBehaviour
{
    public static Storyboard Instance { get; private set; }
    void Awake()
    {
        if (!(Instance == null || Instance == this))
        {
            Destroy(this);
        }
        Instance = this;
    }

    private void OnEnable()
    {
        if (!(Instance == null || Instance == this))
        {
            Destroy(this);
            Debug.LogWarning("Storyboard Instance Destroyed");
        }
        Instance = this;
    }

    private void OnDisable()
    {
        Instance = null;
    }

    //////////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////

    TextMessage rootMessage;
    TextMessage lastSentMessage;

    public string filepath = "Assets/txt files/error.txt";

    private List<TextMessage> messages = new List<TextMessage>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //filepath = Path.GetFullPath(filepath);

        if(!File.Exists(filepath))
        {
            Debug.Log("This file doesn't exist.");
            return;
        }

        ParseTxtFile();
        FillMessages();
        SendInitialMessage();
    }

    private List<TextMessage> ParseTxtFile()
    {
         
        bool begun = false;
        using (StreamReader sr = File.OpenText(filepath))
        {
            string s;
            while ((s = sr.ReadLine()) != null)
            {
                if (s == "[BEGIN]") { begun = true; continue; } // indicates the messages section of the night has begun
                if (begun == false) { continue; }

                string startToken = s.Substring(0, 3);

                if(startToken.Length < 3) { continue; }

                switch (startToken)
                {
                    case "P: ": // partner indicator
                        //Debug.Log("Partner: " + s.Substring(3));
                        messages.Add(new PartnerMessage(s.Substring(3)));
                        break;
                    case "Y: ": // player indicator
                        //Debug.Log("You: " + s.Substring(3));
                        messages.Add(new PlayerMessage(s.Substring(3)));
                        break;
                    default:
                        Debug.Log("Invalid Start Token: '" + startToken + "'");
                        break;
                
                }

            }
        }

        return messages;
    }

    private void FillMessages()
    {
        //rootMessage = new PartnerMessage("Hey babe, how's it going?");


        //PlayerMessage p1;
        //PlayerMessage p2;
        //PlayerMessage p3;

        //rootMessage.SetNextMessages(new PlayerMessage[] {p1 = new PlayerMessage("It's going alright, how are you doing?"), p2 = new PlayerMessage("A bit tired, but fine overall"), p3 = new PlayerMessage("Stop texting me..."), });

        //PartnerMessage partner;

        //p1.SetNextMessages(partner = new PartnerMessage("I'm doing well. I'll be home soon!"));
        //partner.SetNextMessages(new PlayerMessage[] { new PlayerMessage("Great! See you then!"), new PlayerMessage("I'll be here."), new PlayerMessage("Nice")});

        //p2.SetNextMessages(partner = new PartnerMessage("Mood."));
        //partner.SetNextMessages(new PartnerMessage[] {partner = new PartnerMessage("Can I get you anything on my way home?")});
        //partner.SetNextMessages(new PlayerMessage[] {new PlayerMessage("Pickle chips please!"), new PlayerMessage("Nope! I'm all good."), new PlayerMessage("You're plenty for me <3"), });

        //p3.SetNextMessages(partner = new PartnerMessage("Ok."));
        //partner.SetNextMessages(new PartnerMessage[] { new("Maybe we should see other people...") });

        if(messages.Count == 0)
        {
            rootMessage = new PartnerMessage("[ERROR] There are no messages available.");
            return;
        }

        rootMessage = messages[0];

        Debug.Log("Message " + 0 + ": " + messages[0]);

        Debug.Log("Messages Length: " + messages.Count);

        for(int i = 1; i < messages.Count; ++i)
        {
            Debug.Log("Message " + i + ": " + messages[i]);
                
            messages[i - 1].AddNextMessages(messages[i]);

        }



    }

    private void SendInitialMessage()
    {
        lastSentMessage = rootMessage;
        MessageManager.Instance.SendTextMessage(lastSentMessage);
    }

    public void SendNextMessages()
    {
        switch (lastSentMessage)
        {

            case PlayerMessage:
                PlayerMessage newMessage = lastSentMessage as PlayerMessage;
                if (newMessage.nextMessages == null || newMessage.nextMessages.Count == 0 || newMessage.nextMessages[0] == null) { Debug.Log("No messages left"); lastSentMessage = null; return; }

                lastSentMessage = newMessage.nextMessages[0];
                MessageManager.Instance.SendTextMessage(newMessage.nextMessages[0]);
                
                break;
            case PartnerMessage:
                PartnerMessage message = lastSentMessage as PartnerMessage;
                
                if (message.nextMessages == null || message.nextMessages.Count == 0 || message.nextMessages[0] == null) { Debug.Log("No messages left");  lastSentMessage = null; return; }
                
                if(message.nextMessages.Count == 1) 
                { 
                    lastSentMessage = message.nextMessages[0];
                    MessageManager.Instance.SendTextMessage(message.nextMessages[0]); 
                }
                else
                {
                    DialogueSelector.Instance.ReceiveOptions(message.nextMessages);
                }
                
                break;
            default:
                Debug.LogWarning("Um... this isn't supposed to happen twin");
                break;
        
        }

    }

    public void SetLastMessage(TextMessage message)
    {
        lastSentMessage = message;
    }
}
