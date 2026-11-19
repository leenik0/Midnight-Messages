using UnityEngine;
public class Calendar : MonoBehaviour
{
    [Tooltip("Day out of 28 because this is set in the perfect month of FEBRURARY")]
    public int currentDay = 1;
    [Tooltip("Everyday Flooded with Events")]
    public CalendarDay[] calendar;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }
}
