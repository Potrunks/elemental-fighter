using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu instance;
    public bool isPaused = false;
    public GameObject pauseMenuUI;
    public GameObject endGameResults;
    public GameObject scoreList;
    public GameObject winner;
    public GameObject scorePlayer1;
    public GameObject scorePlayer2;
    public GameObject timer;
    public GameObject pauseMenu;
    public GameObject optionMenu;
    public AudioManager audioManager;
    public Slider mainSlider;
    private int playerPauseTheGame;
    public GameObject firstButtonSelectedAfterPauseButton, firstButtonSelectedAfterEndGame, firstButtonSelectedAfterOptionButton, firstButtonSelectedAfterBackButton;

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
        mainSlider.value = GameManager.instance.volumeMainTheme;
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
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstButtonSelectedAfterPauseButton);
        isPaused = true;
        StopGameBackground();
    }

    public void EndTheGame()
    {
        endGameResults.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstButtonSelectedAfterEndGame);
        StopGameBackground();
    }

    private void StopGameBackground()
    {
        scorePlayer1.SetActive(false);
        scorePlayer2.SetActive(false);
        timer.SetActive(false);
        audioManager.DecreaseVolume("MainTheme", 50);
        Time.timeScale = 0f;
        GameManager.instance.timeIsActivated = false;
    }

    public void ResumeTheGame()
    {
        pauseMenuUI.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        scorePlayer1.SetActive(true);
        scorePlayer2.SetActive(true);
        timer.SetActive(true);
        audioManager.RestoreOriginVolume("MainTheme");
        Time.timeScale = 1f;
        isPaused = false;
        GameManager.instance.timeIsActivated = true;
    }

    public void QuitButtonScript()
    {
        ResumeTheGame();
        GameManager.instance.timeIsActivated = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void OptionButtonScript()
    {
        pauseMenu.SetActive(false);
        optionMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstButtonSelectedAfterOptionButton);
    }

    public void BackToPauseMenu()
    {
        optionMenu.SetActive(false);
        pauseMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstButtonSelectedAfterBackButton);
    }

    public void SetMainThemeVolume(float sliderVolumeMainTheme)
    {
        GameManager.instance.volumeMainTheme = sliderVolumeMainTheme;
        Sound sound = audioManager.FindSoundByName("MainTheme");
        if (sound != null)
        {
            sound.audioSource.volume = GameManager.instance.volumeMainTheme;
        }
    }

    public void ReplayButtonScript()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        GameManager.instance.currentTime = GameManager.instance.timeCondition;
        ResumeTheGame();
    }
}
