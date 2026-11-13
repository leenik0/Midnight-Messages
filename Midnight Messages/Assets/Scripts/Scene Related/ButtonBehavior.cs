using UnityEngine;

public class ButtonBehavior : MonoBehaviour
{
    //public GameObject homeScreen;
    //public GameObject messengerApp;

    public GameObject[] screens;
    public GameObject mainScreen;
    public void GoHome()
    {
        //homeScreen.SetActive(true);
        //messengerApp.SetActive(false);

        mainScreen.SetActive(true);

        for(int i = 0; i < screens.Length; ++i)
        {
            screens[i].SetActive(false);
        }
        
        Debug.Log("Switched to Home Screen");
    }

    public void OpenScreen(int index)
    {
        //messengerApp.SetActive(true);
        //homeScreen.SetActive(false);

        screens[index].SetActive(true);
        mainScreen.SetActive(false);

        Debug.Log("Switched to " + screens[index].name + " App");
    }
}
