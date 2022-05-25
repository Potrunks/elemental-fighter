using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public GameObject optionMenu;
    public GameObject fightModeChoice;
    public ToggleGroup victoryConditionToggleGroup;

    public void PlayButtonScript()
    {
        /*
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        GameManager.instance.currentTime = GameManager.instance.timeCondition;
        GameManager.instance.timeIsActivated = true;
        */
        this.gameObject.SetActive(false);
        fightModeChoice.SetActive(true);
    }

    public void QuitButtonScript()
    {
        Application.Quit();
    }

    public void OptionButtonScript()
    {
        this.gameObject.SetActive(false);
        optionMenu.SetActive(true);
    }
}