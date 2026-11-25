using TMPro;
using UnityEngine;

public class HourCounter : MonoBehaviour
{
    public int hour = 21;
    private int hoursPassed = 0;
    public float secondsPerHour = 60f;
    public TMP_Text clockText;
    public ExhaustionMeter exhaustionMeter;

    private float timer = 0f;

    void Start()
    {
        UpdateClockUI();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= secondsPerHour)
        {
            timer -= secondsPerHour;
            hour = (hour + 1) % 24;
            hoursPassed++;
            UpdateClockUI();
            if (hoursPassed > 3)
            {
                exhaustionMeter.adjustExhaustion(2);
                Debug.Log("Exhaustion: " + exhaustionMeter.getStatus());
            }
        }
    }

    void UpdateClockUI()
    {
        if (clockText == null) return;

        string period = hour >= 12 ? "PM" : "AM";
        int displayHour = hour % 12;
        if (displayHour == 0) displayHour = 12; // 0 = 12 AM

        clockText.text = $"{displayHour}:00 {period}";
    }

    public int getHoursPassed()
    {
        return hoursPassed;
    }
}
