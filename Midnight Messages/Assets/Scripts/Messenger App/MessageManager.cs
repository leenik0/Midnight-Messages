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
    public GameObject bufferBottom;
    // allows adding messages to the dialogue option bar (GiveOptions())
    public GameObject optionBar;


    [Header("Message Options")]
    public SentMessage messagePrefab;
    public Color playerMessageColor = Color.lightBlue;
    public Color partnerMessageColor = Color.lightGray;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void GiveOptions(PlayerMessage[] options)
    {

    }

    public void SendMessage(TextMessage message)
    {
        Color messageColor;
        bool rightAlign;
        switch(message)
        {

            case PlayerMessage:
                messageColor = playerMessageColor;
                rightAlign = true;
                break;
            case PartnerMessage:
                messageColor = partnerMessageColor;
                rightAlign = false;
                break;
            default:
                Debug.Log("There's been an error sending a message");
                messageColor = Color.red;
                rightAlign = true;
                break;

        }
        SentMessage sentMessage = Instantiate(messagePrefab, scrollContent.transform);
        sentMessage.SetMessageProperties(message.messageBody, messageColor, rightAlign);

        if(bufferBottom)
        {
            bufferBottom.transform.SetAsLastSibling();
        }

    }

    public void SendTestMessages()
    {
        //// PLAYER
        //SentMessage sentMessage = Instantiate(messagePrefab, scrollContent.transform);
        //sentMessage.SetMessageProperties("September 30th, 1998", playerMessageColor, true);

        //// PARTNER
        //sentMessage = Instantiate(messagePrefab, scrollContent.transform);
        //sentMessage.SetMessageProperties("What", partnerMessageColor, false);

        //// PLAYER
        //sentMessage = Instantiate(messagePrefab, scrollContent.transform);
        //sentMessage.SetMessageProperties("It's a day I'll never forget.", playerMessageColor, true);

        //// PARTNER
        //sentMessage = Instantiate(messagePrefab, scrollContent.transform);
        //sentMessage.SetMessageProperties("You weren't even alive in '98", partnerMessageColor, false);

        //// PLAYER
        //sentMessage = Instantiate(messagePrefab, scrollContent.transform);
        //sentMessage.SetMessageProperties("The cop inside me died that day.", playerMessageColor, true);

        //// PARTNER
        //sentMessage = Instantiate(messagePrefab, scrollContent.transform);
        //sentMessage.SetMessageProperties("You dropped out of high school..", partnerMessageColor, false);

        //// PLAYER
        //sentMessage = Instantiate(messagePrefab, scrollContent.transform);
        //sentMessage.SetMessageProperties("And that night, Raccoon City was wiped out. Thanks to the bioweapons created by Umbrella.", playerMessageColor, true);

        //// PARTNER
        //sentMessage = Instantiate(messagePrefab, scrollContent.transform);
        //sentMessage.SetMessageProperties("wtf is umbrella???", partnerMessageColor, false);

        //// PARTNER
        //sentMessage = Instantiate(messagePrefab, scrollContent.transform);
        //sentMessage.SetMessageProperties("and where even is raccoon city?", partnerMessageColor, false);

        //// PLAYER
        //sentMessage = Instantiate(messagePrefab, scrollContent.transform);
        //sentMessage.SetMessageProperties("Somehow, I made it out. But too many others...weren't so lucky.", playerMessageColor, true);

        //// PARTNER
        //sentMessage = Instantiate(messagePrefab, scrollContent.transform);
        //sentMessage.SetMessageProperties("I can't take you seriously anymore.", partnerMessageColor, false);

        //// PLAYER
        //sentMessage = Instantiate(messagePrefab, scrollContent.transform);
        //sentMessage.SetMessageProperties("I was 'asked' later to join a top-secret government program.", playerMessageColor, true);

        //// PARTNER
        //sentMessage = Instantiate(messagePrefab, scrollContent.transform);
        //sentMessage.SetMessageProperties("Yeah. Sure. Whatever", partnerMessageColor, false);

        //// PLAYER
        //sentMessage = Instantiate(messagePrefab, scrollContent.transform);
        //sentMessage.SetMessageProperties("Not that I had a choice.", playerMessageColor, true);

        //// PLAYER
        //sentMessage = Instantiate(messagePrefab, scrollContent.transform);
        //sentMessage.SetMessageProperties("The training, the punishing missions nearly killed me.", playerMessageColor, true);

        //// PLAYER
        //sentMessage = Instantiate(messagePrefab, scrollContent.transform);
        //sentMessage.SetMessageProperties("But at least they kept my mind off everything.", playerMessageColor, true);

        //// PARTNER
        //sentMessage = Instantiate(messagePrefab, scrollContent.transform);
        //sentMessage.SetMessageProperties("yeah like your partner", partnerMessageColor, false);

        //// PLAYER
        //sentMessage = Instantiate(messagePrefab, scrollContent.transform);
        //sentMessage.SetMessageProperties("If I could just forget what happened that night, the pain—even for a second.", playerMessageColor, true);

        //// PLAYER
        //sentMessage = Instantiate(messagePrefab, scrollContent.transform);
        //sentMessage.SetMessageProperties("This time, it can be different.", playerMessageColor, true);

        //// PARTNER
        //sentMessage = Instantiate(messagePrefab, scrollContent.transform);
        //sentMessage.SetMessageProperties("I want to breakup.", partnerMessageColor, false);

        //// PLAYER
        //sentMessage = Instantiate(messagePrefab, scrollContent.transform);
        //sentMessage.SetMessageProperties("It has to.", playerMessageColor, true);


        if (bufferBottom)
        {
            bufferBottom.transform.SetAsLastSibling();
        }
    }
}
