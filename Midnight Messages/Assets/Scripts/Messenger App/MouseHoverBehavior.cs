
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseHoverBehavior : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public GameObject item;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(!item)
        {
            item = transform.GetChild(0).gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData data)
    {
        item.SetActive(true);
    }
    public void OnPointerExit(PointerEventData data)
    {
        item.SetActive(false);
    }
}
