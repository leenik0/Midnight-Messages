using TMPro;
using UnityEngine;
using UnityEngine.UI;

// a MonoBehaviour class that handles the formatting and display of individual TextMessage instances
public class SentMessage : MonoBehaviour
{
    // the maximum number of characters per line
    //private static float maxMessageWidth = 20;
    private string messageBody;
    private RectTransform rect;

    public TMP_Text textComp;
    public Image imageComp;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(!textComp) { textComp = GetComponentInChildren<TMP_Text>(); }

        if (!imageComp) { imageComp = GetComponentInChildren<Image>(); }

        rect = GetComponent<RectTransform>();
        if(rect == null) { rect = ((RectTransform)(transform)); }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMessageProperties(string message, Color color, bool rightAligned = true, float MULT = 0.03025f)
    {
        textComp.text = message;
        messageBody = message;

        // makes textInfo.lineCount actually work lmao
        textComp.ForceMeshUpdate();

        imageComp.color = color;

        RectTransform imageRectTransform = ((RectTransform)(imageComp.transform));

        Debug.Log("Line Count: " + textComp.textInfo.lineCount);

        if(textComp.textInfo.lineCount > 1)
        {
            // adjust height of bubble so the text fits
            ((RectTransform)transform).sizeDelta = new Vector2(((RectTransform) transform).rect.width, ((RectTransform)transform).rect.height + 0.05f * (textComp.textInfo.lineCount - 1));
        }
        else
        {
            // make it so the width shrinks if the message is short
            imageRectTransform.sizeDelta = new Vector2(0.03025f * (textComp.text.Length + 2), imageRectTransform.rect.height);
            
            imageRectTransform.offsetMin= new Vector2(imageRectTransform.offsetMin.x, 0);
            imageRectTransform.offsetMax= new Vector2(imageRectTransform.offsetMax.x, 0);

            //((RectTransform)textComp.transform).sizeDelta = new Vector2(0.5975f / 0.65f * imageRectTransform.rect.width, imageRectTransform.rect.height);
        }


        if (rightAligned)
        {
            // set pivot right
            //imageRectTransform.pivot = new Vector2(1f, 0.5f);
            imageRectTransform.pivot = new Vector2(1f, 0.5f);
            imageRectTransform.anchorMin = new Vector2(1, 0);
            imageRectTransform.anchorMax = new Vector2(1, 1);
            // set proper position
            imageRectTransform.localPosition = new Vector3(0.475f, 0, 0);
        }
        else
        {
            // set pivot left
            imageRectTransform.pivot = new Vector2(0f, 0.5f);
            imageRectTransform.anchorMin = new Vector2(0, 0);
            imageRectTransform.anchorMax = new Vector2(0, 1);
            // set proper position
            imageRectTransform.localPosition = new Vector3(-0.425f, 0, 0);
        }

        

        // sets name to message info
        gameObject.name = "Sent Message: " + (message.Length <= 10 ? message : message[..10] + "...");
    }
}
