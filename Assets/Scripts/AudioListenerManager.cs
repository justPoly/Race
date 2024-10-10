using UnityEngine;

public class AudioListenerManager : MonoBehaviour
{
    void Awake()
    {
        // Check if more than one Audio Listener exists
        AudioListener[] listeners = FindObjectsOfType<AudioListener>();
        if (listeners.Length > 1)
        {
            // Disable additional listeners, keep the first one active
            for (int i = 1; i < listeners.Length; i++)
            {
                listeners[i].enabled = false;
            }
        }
    }
}
