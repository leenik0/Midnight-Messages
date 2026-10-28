using UnityEngine;

public class PartnerMessage : TextMessage
{
    public PartnerMessage(string message, float writeTime = 0) : base(message)
    {
        this.writeTime = writeTime;
    }

    public PartnerMessage() : base("This is a test partner message.")
    {
        writeTime = 0;
    }

    // the time it takes for the message to be sent, where a typing animation is displayed
    public float writeTime = 0;
}
