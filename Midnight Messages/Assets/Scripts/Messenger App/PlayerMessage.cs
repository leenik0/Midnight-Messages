using JetBrains.Annotations;
using TMPro;
using UnityEngine;

// a class that stores the information regarding player messages
//[CreateAssetMenu]
public class PlayerMessage : TextMessage
{


    public PlayerMessage(string message, int relDeltReg = 0, string messageRevealed = "", int relDeltRev = 0) : base(message)
    {
        relationshipDeltaRegular = relDeltReg;
        messageBodyRevealed = messageRevealed;
        relationshipDeltaRevealed = relDeltRev;
    }

    // empty constructor for testing purposes
    public PlayerMessage() : base ("This is a test player message.")
    {
        relationshipDeltaRegular = 0;
        messageBodyRevealed = "Revealed message test.";
        relationshipDeltaRevealed = 0;
    }


    [Header("Player Message Variables")]

    // how much the main message impacts the relationship status
    [Range(-10,10)]
    public int relationshipDeltaRegular;
    
    // how much the revealed message impacts the relationship status
    [Range(-10,10)]
    public int relationshipDeltaRevealed;

    // what the message turns into in the morning if the player is sleep deprived.
    public string messageBodyRevealed;

}
