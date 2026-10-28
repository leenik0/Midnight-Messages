using JetBrains.Annotations;
using TMPro;
using UnityEngine;

// a class that stores the information regarding player messages
[CreateAssetMenu]
public class PlayerMessage : TextMessage
{

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
