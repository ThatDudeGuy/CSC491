using UnityEngine;
using UnityEngine.SceneManagement; // For scene management
using UnityEngine.UI; // For UI elements

public class PauseMenu : MonoBehaviour
{
public Rigidbody rb;


    public GameObject pauseMenuUI; // Assign your pause menu panel here
    private bool isPaused = false;


    void start(){
       // rb = GetComponent<Rigidbody>();
        pauseMenuUI.SetActive(false); // pause menu is hidden at the start

        // Ensure the Rigidbody is active and behaving normally at the start
        if (rb != null)
        {
            rb.isKinematic = false; // Start with normal physics interactions
        }
    }


    void Update()
    {
        // Check for pause input (Escape key)
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }

        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; // Resume game time
        isPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; // Pause game time
        isPaused = true;
    }

    public void Quit()
    {
        // Load the main menu or exit the game
        // Uncomment the following line if you have a main menu scene
        // SceneManager.LoadScene("MainMenu");
        Application.Quit();
    }


    

    // Optional: Quit button functionality
    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Stop the game in the editor
        #else
        Application.Quit(); // Quit the game
        #endif
    }
}


