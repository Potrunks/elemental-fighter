using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu instance;
    private bool isPaused = false;
    public GameObject pauseMenuUI;
    private int playerPauseTheGame;

    private void Awake()
    {
        instance = this;
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
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeTheGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void QuitButtonScript()
    {
        ResumeTheGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
