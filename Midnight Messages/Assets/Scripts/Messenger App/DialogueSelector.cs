using System.Collections;
using System.Collections.Generic;
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

    private void OnEnable()
    {
        if (!(Instance == null || Instance == this))
        {
            Destroy(this);
            Debug.LogWarning("DialogueSelector Instance Destroyed");
        }
        Instance = this;
    }

    private void OnDisable()
    {
        Instance = null;
    }



    public GameObject content;
    public GameObject[] options = new GameObject[3];
    public SentMessage optionPrefab;

    // the game object that provides a buffer between the last sent message and the dialogue option bar
    public GameObject bufferBottom;


    void Start()
    {
        ClearOptions();
    }

    public void ReceiveOptions(List<TextMessage> choices)
    {
        //Debug.Log("Received Options");
        //foreach(TextMessage choice in choices)
        //{
        //    if(choice == null) { continue; }
        //    SentMessage sentMessage = Instantiate(optionPrefab, content.transform);
        //    sentMessage.SetMessageProperties(choice, false);
        //}

        for(int i = 0; i < 3 && i < choices.Count; ++i)
        {
            SentMessage optionChild = options[i].transform.GetChild(0).GetComponent<SentMessage>();
            StartCoroutine(SetProperties(optionChild, choices[i], false));
            
            options[i].SetActive(true); // make the icon visible
        }

        if(bufferBottom)
        {
            bufferBottom.transform.SetAsLastSibling();
        }
    }

    public void ClearOptions()
    {
        foreach(GameObject option in options)
        {
            option.SetActive(false);
        }
    }

    public IEnumerator SetProperties(SentMessage optionChild, TextMessage message, bool align)
    {
        yield return new WaitUntil(() => optionChild.gameObject.activeSelf);
        optionChild.SetMessageProperties(message, align);
    }
}
