using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu instance;
    public bool isPaused = false;
    public GameObject pauseMenuUI;
    public GameObject scorePlayer1;
    public GameObject scorePlayer2;
    public AudioManager audioManager;
    private int playerPauseTheGame;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void PauseGame(int playerIndex)
    {
        if (isPaused == false)
        {
            PauseTheGame(playerIndex);
        }
        else if (isPaused == true && playerIndex == playerPauseTheGame)
        {
            ResumeTheGame();
        }
    }

    private void PauseTheGame(int playerIndex)
    {
        playerPauseTheGame = playerIndex;
        pauseMenuUI.SetActive(true);
        scorePlayer1.SetActive(false);
        scorePlayer2.SetActive(false);
        audioManager.DecreaseVolume("MainTheme", 50);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeTheGame()
    {
        pauseMenuUI.SetActive(false);
        scorePlayer1.SetActive(true);
        scorePlayer2.SetActive(true);
        audioManager.RestoreOriginVolume("MainTheme");
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void QuitButtonScript()
    {
        ResumeTheGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
