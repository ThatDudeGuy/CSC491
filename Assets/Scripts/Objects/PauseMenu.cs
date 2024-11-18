using UnityEngine;
using UnityEngine.SceneManagement; // For scene management
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public Rigidbody rb;
    public bool hello;


    public GameObject pauseMenuUI, gameCanvas, player;
    private bool isPaused = false;


    void Start(){
       // rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        pauseMenuUI.SetActive(false); // pause menu is hidden at the start

        // Ensure the Rigidbody is active and behaving normally at the start
        if (rb != null)
        {
            rb.isKinematic = false; // Start with normal physics interactions
        }
    }


    void Update()
    {
        if(!gameObject.activeSelf) print("Goodbye");
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
                // hello = true;
            }

        }

        // if(hello) print("Pause Updates");
    }

    public void Resume()
    {
        player.GetComponent<Player_Movement>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f; // Resume game time
        pauseMenuUI.SetActive(false);
        gameCanvas.SetActive(true);
        isPaused = false;
    }

    public void Pause()
    {
        player.GetComponent<Player_Movement>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        pauseMenuUI.SetActive(true);
        gameCanvas.SetActive(false);
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

    public void Clicked(){
        print("Resume");
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


