using UnityEngine;
using UnityEngine.UI;

public class RaceFinish : MonoBehaviour
{
    private GameManager gameManager;

    // Finish camera gets activated
    [SerializeField] private GameObject FinishCam;

    // ViewModes get deactivated so you can't change the camera once the race is over
    [SerializeField] private GameObject ViewModes;

    // Race UI gets deactivated once the race finishes
    [SerializeField] private GameObject PosDisplay, PauseButton, Panel1, Panel2;

    // The different finish panels (if you win the race or lose)
    [SerializeField] private GameObject FinishPanelWin, FinishPanelLose;

    void Start()
    {
        // Get reference to GameManager to access the reward system
        gameManager = FindObjectOfType<GameManager>();
    }

    void OnTriggerEnter()
    {
        this.GetComponent<BoxCollider>().enabled = false; // Disable trigger to avoid multiple activations
        FinishCam.SetActive(true); // Activate finish camera

        // Deactivate the race UI elements
        PauseButton.SetActive(false);
        Panel1.SetActive(false);
        Panel2.SetActive(false);
        ViewModes.SetActive(false);

        // Handle player's position
        HandlePlayerFinish(PosDisplay.GetComponent<Text>().text);
    }

    private void HandlePlayerFinish(string positionText)
    {
        // Switch statement to handle rewards and UI display based on the player's position
        switch (positionText)
        {
            case "1st Place":
                RewardPlayer(1);
                ShowFinishPanel(true); // Show win panel
                break;

            case "2nd Place":
                RewardPlayer(2);
                ShowFinishPanel(true); // Show win panel
                break;

            case "3rd Place":
                RewardPlayer(3);
                ShowFinishPanel(true); // Show win panel
                break;

            default:
                // Any position other than top 3
                ShowFinishPanel(false); // Show lose panel
                EndRace();
                break;
        }
    }

    private void RewardPlayer(int position)
    {
        // Use GameManager's SimulateRace to give rewards
        gameManager.SimulateRace(position);
    }

    private void ShowFinishPanel(bool win)
    {
        // Toggle win/lose panels based on the race outcome
        FinishPanelWin.SetActive(win);
        FinishPanelLose.SetActive(!win);
    }

    private void EndRace()
    {
        // Additional logic for ending the race (like stopping audio and time)
        AudioListener.volume = 0f; // Turn off audio
        Time.timeScale = 0; // Pause the game
    }
}
