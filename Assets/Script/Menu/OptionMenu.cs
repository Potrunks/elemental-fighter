using UnityEngine;

public class OptionMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public AudioManager audioManager;
    public void BackButtonScript()
    {
        this.gameObject.SetActive(false);
        mainMenu.SetActive(true);
    }
    public void SetMainThemeVolume(float sliderVolumeMainTheme) {
        GameManager.instance.volumeMainTheme = sliderVolumeMainTheme;
        Sound sound = audioManager.FindSoundByName("MainTheme");
        if(sound != null) {
            sound.audioSource.volume = GameManager.instance.volumeMainTheme;
        }
    }
}
