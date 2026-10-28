using UnityEngine;

public class DialogueSelector : MonoBehaviour
{
    // Implements the Singleton Pattern
    public static DialogueSelector Instance { get; private set; }
    void Awake()
    {
        if (!(Instance == null || Instance == this))
        {
            Destroy(this);
        }
        Instance = this;
    }

    public GameObject content;
    public SentMessage optionPrefab;

    // the game object that provides a buffer between the last sent message and the dialogue option bar
    public GameObject bufferBottom;


    public void ReceiveOptions(TextMessage[] options)
    { 
        foreach(TextMessage option in options)
        {
            if(option == null) { continue; }
            SentMessage sentMessage = Instantiate(optionPrefab, content.transform);
            sentMessage.SetMessageProperties(option, false);
        }

        if(bufferBottom)
        {
            bufferBottom.transform.SetAsLastSibling();
        }
    }

    public void ClearOptions()
    {
        foreach(Transform child in content.transform)
        {
            if(child.GetComponent<SentMessage>())
                Destroy(child.gameObject);
        }
    }
}
