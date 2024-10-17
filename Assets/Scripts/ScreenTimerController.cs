using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenTimerController : MonoBehaviour
{
    public float splashDuration = 3.0f; // Duration of the splash screen

    void Start()
    {
        // Call the LoadNextScene function after splashDuration seconds
        Invoke("LoadNextScene", splashDuration);
    }

    void LoadNextScene()
    {
        // Replace "MainScene" with the name of your game scene
        SceneManager.LoadScene("Splash Screen");
    }
}
