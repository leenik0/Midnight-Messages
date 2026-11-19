using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CalendarDay : MonoBehaviour
{
    public Event[] events;
    public int day;
    public TextMeshProUGUI dayText;

    void Start()
    {
        dayText = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        dayText.text = day.ToString();
    }

}
