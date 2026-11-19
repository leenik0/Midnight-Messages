using UnityEngine;

[CreateAssetMenu(menuName = "Game Data/Relationship Meter")]
public class RelationshipBehavior : ScriptableObject
{
    private int relationshipStatus = 0;

    public int getStatus()
    {
        return relationshipStatus;
    }

    public void alterStatusValue(int val)
    {
        Debug.Log("status increased by" + val);
        relationshipStatus += val;
    }
}
