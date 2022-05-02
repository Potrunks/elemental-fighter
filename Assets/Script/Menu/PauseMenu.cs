using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu instance;
    public bool isPaused = false;
    public GameObject pauseMenuUI;
    public GameObject scorePlayer1;
    public GameObject scorePlayer2;
    public GameObject timer;
    public GameObject pauseMenu;
    public GameObject optionMenu;
    public AudioManager audioManager;
    public Slider mainSlider;
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
        scorePlayer1.SetActive(false);
        scorePlayer2.SetActive(false);
        timer.SetActive(false);
        audioManager.DecreaseVolume("MainTheme", 50);
        Time.timeScale = 0f;
        isPaused = true;
        GameManager.instance.timeIsActivated = false;
    }

    public void ResumeTheGame()
    {
        pauseMenuUI.SetActive(false);
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
    }

    public void BackToPauseMenu()
    {
        optionMenu.SetActive(false);
        pauseMenu.SetActive(true);
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
}
