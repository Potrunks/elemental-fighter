using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject optionMenu;
    public void PlayButtonScript()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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