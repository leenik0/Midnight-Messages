using UnityEngine;

// a class that manages adding messages to the viewport and the dialogue selection bar
public class MessageManager : MonoBehaviour
{
    // Implements the Singleton Pattern
    public static MessageManager Instance { get; private set; }
    void Awake()
    {
        if (!(Instance == null || Instance == this))
        {
            Destroy(this);
        }
        Instance = this;
    }

    [Header("Destination Objects")]
    // allows adding messages to the content (SendMessage())
    public GameObject scrollContent;
    // the game object that provides a buffer between the last sent message and the dialogue option bar
    public GameObject bufferBottom;


    [Header("Message Options")]
    public SentMessage messagePrefab;
    public Color playerMessageColor = Color.lightBlue;
    public Color partnerMessageColor = Color.lightGray;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    //public void GiveOptions(PlayerMessage[] options)
    //{
    //    DialogueSelector.Instance.ReceiveOptions(options);
    //}

    public void SendTextMessage(TextMessage message, bool align = true)
    {
        if(message == null)
        {
            message = Random.Range(0,10) % 2 == 0 ? new PlayerMessage() : new PartnerMessage();
        }
        
        //TODO: add an animation for the typing partner messages


        SentMessage sentMessage = Instantiate(messagePrefab, scrollContent.transform);
        sentMessage.SetMessageProperties(message, align);

        if(bufferBottom)
        {
            bufferBottom.transform.SetAsLastSibling();
        }

    }

    // Alternate Debug Tool
    public void Click()
    {
        SendTextMessage(null);
    }

    public void SendDebugMessages()
    {
        SendTextMessage(new PlayerMessage("September 30th, 1998"));

        SendTextMessage(new PartnerMessage("It's a day I'll never forget..."));

        SendTextMessage(new PlayerMessage("The cop inside me died that day."));

        SendTextMessage(new PartnerMessage("And that night, Raccoon City was wiped out. Thanks to the bioweapons created by Umbrella."));
    }
}
