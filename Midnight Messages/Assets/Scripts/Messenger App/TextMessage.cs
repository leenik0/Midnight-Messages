using UnityEngine;

// a generic class that extends ScriptableObject to store message information
public class TextMessage
{
    [Header("Text Message Variables")]
    public string messageBody;
    public TextMessage[] nextMessages;

    public TextMessage(string message)
    {
        messageBody = message;
    }

    public void SetMessageBody(string messageBody)
    {
        this.messageBody = messageBody;
    }

    public void SetNextMessages(TextMessage[] messages)
    {
        nextMessages = messages;
    }

    public void SetNextMessages(TextMessage message)
    {
        nextMessages = new TextMessage[] { message };
    }
}
