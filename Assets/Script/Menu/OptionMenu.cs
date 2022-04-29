using UnityEngine;

public class OptionMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public void BackButtonScript() {
        this.gameObject.SetActive(false);
        mainMenu.SetActive(true);
    }
}
