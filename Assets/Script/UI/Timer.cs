using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private TextMeshProUGUI timerText;

    void Start()
    {
        timerText = GetComponent<TextMeshProUGUI>();
        timerText.text = "";
    }

    void Update()
    {
        if (GameManager.instance.timeCondition != -1)
        {
            DisplayTime(GameManager.instance.currentTime);
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timerText.text = Mathf.FloorToInt(timeToDisplay).ToString();
    }
}
