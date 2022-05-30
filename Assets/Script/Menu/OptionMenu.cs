using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;
using UnityEngine.EventSystems;

public class OptionMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public Slider mainSlider;
    public AudioManager audioManager;
    public ToggleGroup victoryConditionToggleGroup;
    public ToggleGroup timeConditionToggleGroup;
    public GameObject firstButtonSelectedAfterApplyButton;
    private void Awake()
    {
        mainSlider.value = GameManager.instance.volumeMainTheme;
    }
    public void ApplyButtonScript()
    {
        UpdateVictoryCondition();
        UpdateTimeCondition();
        this.gameObject.SetActive(false);
        mainMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstButtonSelectedAfterApplyButton);
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

    private void UpdateVictoryCondition()
    {
        Toggle victoryConditionToggleSelected = victoryConditionToggleGroup.ActiveToggles().FirstOrDefault();
        GameManager.instance.victoryPointCondition = int.Parse(victoryConditionToggleSelected.GetComponentInChildren<TextMeshProUGUI>().text);
    }

    private void UpdateTimeCondition()
    {
        Toggle timeConditionToggleSelected = timeConditionToggleGroup.ActiveToggles().FirstOrDefault();
        if (timeConditionToggleSelected.GetComponentInChildren<TextMeshProUGUI>().text.Equals("Infini"))
        {
            GameManager.instance.timeCondition = -1;
        }
        else
        {
            GameManager.instance.timeCondition = int.Parse(timeConditionToggleSelected.GetComponentInChildren<TextMeshProUGUI>().text);
        }
    }
}
