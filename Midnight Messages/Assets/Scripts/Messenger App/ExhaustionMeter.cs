using UnityEngine;


[CreateAssetMenu(menuName = "Game Data/Exhaustion Meter")]
public class ExhaustionMeter : ScriptableObject
{
    private int exhaustionStatus = 0;

    public int getStatus()
    {
        return exhaustionStatus;
    }

    public void alterStatus(int val)
    {
        exhaustionStatus += val;
    }
}
