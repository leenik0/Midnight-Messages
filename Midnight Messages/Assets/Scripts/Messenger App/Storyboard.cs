using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Assemblies;


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

    TextMessage rootMessage;
    TextMessage lastSentMessage;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        FillMessages();
        SendInitialMessage();
    }

    private void FillMessages()
    {
        rootMessage = new PartnerMessage("Hey babe, how's it going?");


        PlayerMessage p1;
        PlayerMessage p2;
        PlayerMessage p3;
        
        rootMessage.SetNextMessages(new PlayerMessage[] {p1 = new PlayerMessage("It's going alright, how are you doing?"), p2 = new PlayerMessage("A bit tired, but fine overall"), p3 = new PlayerMessage("Stop texting me..."), });

        PartnerMessage partner;

        p1.SetNextMessages(partner = new PartnerMessage("I'm doing well. I'll be home soon!"));
        partner.SetNextMessages(new PlayerMessage[] { new PlayerMessage("Great! See you then!"), new PlayerMessage("I'll be here."), new PlayerMessage("Nice")});

        p2.SetNextMessages(partner = new PartnerMessage("Mood."));
        partner.SetNextMessages(new PartnerMessage[] {partner = new PartnerMessage("Can I get you anything on my way home?")});
        partner.SetNextMessages(new PlayerMessage[] {new PlayerMessage("Pickle chips please!"), new PlayerMessage("Nope! I'm all good."), new PlayerMessage("You're plenty for me <3"), });

        p3.SetNextMessages(partner = new PartnerMessage("Ok."));
        partner.SetNextMessages(new PartnerMessage[] { new("Maybe we should see other people...") });


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
                if (newMessage.nextMessages == null || newMessage.nextMessages.Length == 0 || newMessage.nextMessages[0] == null) { Debug.Log("No messages left"); lastSentMessage = null; return; }

                lastSentMessage = newMessage.nextMessages[0];
                MessageManager.Instance.SendTextMessage(newMessage.nextMessages[0]);
                
                break;
            case PartnerMessage:
                PartnerMessage message = lastSentMessage as PartnerMessage;
                
                if (message.nextMessages == null || message.nextMessages.Length == 0 || message.nextMessages[0] == null) { Debug.Log("No messages left");  lastSentMessage = null; return; }
                
                if(message.nextMessages.Length == 1) 
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
