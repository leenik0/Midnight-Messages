using Subtegral.DialogueSystem.DataContainers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
    
    [SerializeField] private DialogueContainer dialogue;
    private List<TextMessage> messages = new List<TextMessage>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        FillMessages();
        SendInitialMessage();
    }


    private void FillMessages()
    {
        var narrativeData = dialogue.NodeLinks.First();

        CreateChildNodeMessage(narrativeData.TargetNodeGUID);

        rootMessage = messages[messages.Count - 1];

        //foreach(TextMessage message in rootMessage.nextMessages)
        //{
        //    Debug.Log(message);
        //}
    }


    /**
     * 1. Depth First Search Recursively
     * 2. Memoize created Messages
     * 3. Add children nodes as nextMessages
     */
    private void CreateChildNodeMessage(string NodeGUID)
    {
        var node = dialogue.DialogueNodeData.Find(x => x.NodeGUID == NodeGUID);
        var childNodes = dialogue.NodeLinks.Where(x => x.BaseNodeGUID == NodeGUID);

        //Debug.Log("Node Text: " + node.DialogueText);

        foreach(var child in childNodes)
        {
            //Debug.Log("Child Node Text: " + dialogue.DialogueNodeData.Find(x => x.NodeGUID == child.TargetNodeGUID).DialogueText);
            CreateChildNodeMessage(child.TargetNodeGUID);
        }

        /**
         * textTokens Index:
         * 0. Sender Identifier ("P: ", "Y: ", etc.)
         * 1. Relationship Modifier ([+1], [-1], etc.)
         * 2. Message Body Regular ("Hey sweetie! I miss you!")
         * // <- Revealed Message identifier
         * 3. Revealed Relationship Modifier ([+1], [-1], etc.)
         * 4. Message Body Revealed ("I hate you and want you to die...")
         * 
         */
        string[] textTokens = ParseDialogueText(node.DialogueText);

        // checking if node already exists
        foreach(TextMessage message in messages)
        {
            if (message.messageBody == node.DialogueText.Substring(3))
            {
                return;
            }
        }

        TextMessage newMessage = null;
        // creating the respective message
        switch (textTokens[0])
        {
            case "P: ": // partner indicator
                        //Debug.Log("Partner: " + s.Substring(3));
                newMessage = new PartnerMessage(textTokens[2]);
                messages.Add(newMessage);
                break;
            case "Y: ": // player indicator
                int relationshipDeltaRegular = 0;
                int relationshipDeltaRevealed = 0;
                int.TryParse(textTokens[1], out relationshipDeltaRegular);
                int.TryParse(textTokens[3], out relationshipDeltaRevealed);

                Debug.Log("Message: " + textTokens[2] + "TextTokens 3: " + textTokens[3]);

                newMessage = new PlayerMessage(textTokens[2], relationshipDeltaRegular, textTokens[4], relationshipDeltaRevealed);
                messages.Add(newMessage);
                break;
            case "S: ": // skip token for when multiple paths converge into three new options
                foreach (var child in childNodes)
                {
                    CreateChildNodeMessage(child.TargetNodeGUID);
                }
                break;
            default:
                Debug.Log("Invalid Start Token: '" + textTokens[0] + "'");
                newMessage = new PartnerMessage("This shouldn't happen.");
                messages.Add(newMessage);
                break;

        }

        // assigning the nextMessage nodes
        foreach (var child in childNodes)
        {
            TextMessage next = GetMessageFromID(child.TargetNodeGUID);
            if (next != null && newMessage != null)
            {
                newMessage.AddNextMessages(next);
            }
        }
    }

    private TextMessage GetMessageFromID(string NodeGUID)
    {
        TextMessage returnMessage = null;

        string text = dialogue.DialogueNodeData.Find(x => x.NodeGUID == NodeGUID).DialogueText;

        foreach(TextMessage message in messages)
        {
            if(message.messageBody == ParseDialogueText(text)[2])
            {
                returnMessage = message;
                break;
            }
        }

        return returnMessage;
    }

    private string[] ParseDialogueText(string text)
    {
        string[] tokens = new string[5];

        /**
         * Index:
         * 0. Sender Identifier ("P: ", "Y: ", etc.)
         * 1. Relationship Modifier ([+1], [-1], etc.)
         * 2. Message Body Regular ("Hey sweetie! I miss you!")
         * // <- Revealed Message identifier
         * 3. Revealed Relationship Modifier ([+1], [-1], etc.)
         * 4. Message Body Revealed ("I hate you and want you to die...")
         * 
         */

        if(text.Length < 3)
        {
            tokens[0] = "ERR";
            return tokens;
        }

        // Sender Identifier
        tokens[0] = text.Substring(0, 3);
        
        text = text.Substring(3); // cut off first token

        int modifierStartIndex = text.IndexOf("[");
        int modifierEndIndex = text.IndexOf("]");
        int revealedTokenIndex = text.IndexOf("//");
        
        // Relationship Modifier
        if(modifierStartIndex != -1 && modifierEndIndex != -1 && ((revealedTokenIndex == -1) || (modifierEndIndex < revealedTokenIndex))) // if modifier token exists, and is before revealed token ([+X] before //) if revealed exists
        {
            //Debug.Log("Modifier: " + text.Substring(modifierStartIndex + 1, modifierEndIndex - 1));

            tokens[1] = text.Substring(modifierStartIndex + 1, modifierEndIndex - 1); // just get the number
            text = text.Remove(text.IndexOf(tokens[1]) - 1, tokens[1].Length + 2); // for some reason it's start index and count, not start index and end index

            revealedTokenIndex = text.IndexOf("//");
        }
        else
        {
            tokens[1] = null;
        }

        
        // Message Body Regular
        if (revealedTokenIndex != -1)
        {
            tokens[2] = text.Substring(0, revealedTokenIndex).Trim();
            text = text.Substring(revealedTokenIndex + 2); // gets rid of everything before // and also the //
        }
        else
        {
            tokens[2] = text.Trim();

            tokens[3] = null;
            tokens[4] = null;

            return tokens;
        }

        // Revealed Relationship Modifier
        modifierStartIndex = text.IndexOf("[");
        modifierEndIndex = text.IndexOf("]");

        if(modifierStartIndex != -1 && modifierEndIndex != -1)
        {
            tokens[3] = text.Substring(modifierStartIndex + 1, modifierEndIndex - 2);
            text = text.Remove(text.IndexOf(tokens[3]) - 1, tokens[3].Length + 2);
        }
        else
        {
            tokens[3] = null;
        }

        //Message Body Revealed
        tokens[4] = text.Trim(); // everything else lmao


        return tokens;
    }
    private void SendInitialMessage()
    {
        lastSentMessage = rootMessage;
        MessageManager.Instance.SendTextMessage(lastSentMessage);
    }

    public void SendNextMessages()
    {
        List<TextMessage> receiveOptions = new();
        foreach (TextMessage nextMessage in lastSentMessage.nextMessages)
        {
            switch (nextMessage)
            {
                case PartnerMessage:
                    lastSentMessage = nextMessage;
                    MessageManager.Instance.SendTextMessage(nextMessage);
                    break;

                case PlayerMessage:
                    receiveOptions.Add(nextMessage);
                    break;


            }

        }

        if (receiveOptions.Count != 0)
        {
            DialogueSelector.Instance.ReceiveOptions(receiveOptions);
        }
    }

    public void SetLastMessage(TextMessage message)
    {
        lastSentMessage = message;
    }
}
