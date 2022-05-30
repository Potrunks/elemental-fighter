using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    public GameObject optionMenu;
    public GameObject fightModeChoice;
    public ToggleGroup victoryConditionToggleGroup;
    public GameObject firstButtonSelectedAfterPlayButton, firstButtonSelectedAfterOptionButton;

    public void PlayButtonScript()
    {
        /*
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        GameManager.instance.currentTime = GameManager.instance.timeCondition;
        GameManager.instance.timeIsActivated = true;
        */
        this.gameObject.SetActive(false);
        fightModeChoice.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstButtonSelectedAfterPlayButton);
    }

    public void QuitButtonScript()
    {
        Application.Quit();
    }

    public void OptionButtonScript()
    {
        this.gameObject.SetActive(false);
        optionMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstButtonSelectedAfterOptionButton);
    }
}