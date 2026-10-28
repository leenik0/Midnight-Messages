using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTools : MonoBehaviour
{
    // Implements the Singleton Pattern
    public static SceneTools Instance { get; private set; }
    void Awake()
    {
        if (!(Instance == null || Instance == this))
        {
            Destroy(this);
        }
        Instance = this;
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
