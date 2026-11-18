using UnityEngine;
using System.Collections.Generic;
using System.Collections.ObjectModel;

// a generic class that extends ScriptableObject to store message information
public class TextMessage
{
    [Header("Text Message Variables")]
    public string messageBody;
    public List<TextMessage> nextMessages = new();
    //public TextMessage previousMessage = null;

    public TextMessage(string message/*, TextMessage previousMessage = null*/)
    {
        messageBody = message != null ? message : "ERROR: NULL MESSAGE";
        //this.previousMessage = previousMessage;
    }

    public void SetMessageBody(string messageBody)
    {
        this.messageBody = messageBody;
    }

    public void AddNextMessages(Collection<TextMessage> messages)
    {
        nextMessages.AddRange(messages);
    }
    
    public void AddNextMessages(TextMessage[] messages)
    {
        nextMessages.AddRange(messages);
    }

    public void AddNextMessages(TextMessage message)
    {
        nextMessages.Add(message);
    }

    public override string ToString()
    {
        return "MessageBody: " + messageBody + "; NextMessages Count: " + nextMessages.Count;
    }

    //public TextMessage GetPreviousMessage()
    //{
    //    return previousMessage;
    //}

    //public void SetPreviousMessage(TextMessage message)
    //{
    //    previousMessage = message;
    //}
}
