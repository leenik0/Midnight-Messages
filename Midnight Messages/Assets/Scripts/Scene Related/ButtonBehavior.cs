using UnityEngine;

public class ButtonBehavior : MonoBehaviour
{
    public GameObject homeScreen;
    public GameObject messengerApp;

    public void GoHome()
    {
        homeScreen.SetActive(true);
        messengerApp.SetActive(false);
        
        Debug.Log("Switched to Home Screen");
    }

    public void OpenMessengerApp()
    {
        messengerApp.SetActive(true);
        homeScreen.SetActive(false);
        
        Debug.Log("Switched to Messenger App");
    }
}
