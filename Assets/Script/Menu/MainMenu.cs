using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject optionMenu;
    public GameObject fightModeChoice;
    public ToggleGroup victoryConditionToggleGroup;
    public GameObject firstButtonSelectedAfterPlayButton, firstButtonSelectedAfterOptionButton;

    /// <summary>
    /// Allow to go to selection character menu
    /// </summary>
    public void PlayButtonScript()
    {
        SceneManager.LoadScene("CharacterSelection");
    }

    /// <summary>
    /// Quit the Application
    /// </summary>
    public void QuitButtonScript()
    {
        Application.Quit();
    }

    /// <summary>
    /// Allow to go to Option Menu
    /// </summary>
    public void OptionButtonScript()
    {
        this.gameObject.SetActive(false);
        optionMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstButtonSelectedAfterOptionButton);
    }
}