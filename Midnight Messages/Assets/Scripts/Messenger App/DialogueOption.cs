using UnityEngine;
using UnityEngine.UI;

public class DialogueOption : MonoBehaviour
{

    private SentMessage sentMessage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sentMessage = GetComponentInParent<SentMessage>();
    }

    public void Click()
    {
        if(sentMessage == null)
        {
            Debug.LogWarning("SentMessage does not exist in debug option...");
            return;
        }
        MessageManager.Instance.SendTextMessage(sentMessage.GetMessage());
        DialogueSelector.Instance.ClearOptions();
        Storyboard.Instance.SetLastMessage(sentMessage.GetMessage());
        Storyboard.Instance.SendNextMessages();
    }
}
