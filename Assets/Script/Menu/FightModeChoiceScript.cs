using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class FightModeChoiceScript : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject firstButtonSelectedAfterBackButton;

    public void J1VSJ2ModeButtonScript()
    {
        SetSelectedMode(false, false, false, false);
        LoadFightScene();
    }

    public void J1VSAIModeButtonScript()
    {
        SetSelectedMode(false, true, false, false);
        LoadFightScene();
    }

    public void AIVSAIModeButtonScript()
    {
        Debug.Log("AI VS AI mode selected...");
        SetSelectedMode(true, true, true, true);
        LoadFightScene();
    }

    public void backToMainMenuButtonScript()
    {
        Debug.Log("Click on back button to go to the main menu from the fight mode choice");
        this.gameObject.SetActive(false);
        mainMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstButtonSelectedAfterBackButton);
    }

    private void SetSelectedMode(bool j1AIMode, bool j2AIMode, bool j3AIMode, bool j4AIMode)
    {
        GameManager.instance.selectedMode.Clear();
        GameManager.instance.selectedMode.Add(0, j1AIMode);
        GameManager.instance.selectedMode.Add(1, j2AIMode);
        GameManager.instance.selectedMode.Add(2, j3AIMode);
        GameManager.instance.selectedMode.Add(3, j4AIMode);
    }

    private void LoadFightScene()
    {
        GameManager.instance.currentTime = GameManager.instance.timeCondition;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
