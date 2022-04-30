using UnityEngine;
using TMPro;

public class ScorePlayer : MonoBehaviour
{
    private TextMeshProUGUI textScore;
    public int playerIndex;
    public int victoryPoint;
    void Start()
    {
        textScore = GetComponent<TextMeshProUGUI>();
        DisplayScore();
    }

    public void UpdateScore()
    {
        this.victoryPoint++;
        DisplayScore();
    }

    private void DisplayScore()
    {
        if (playerIndex == 0)
        {
            textScore.text = "Player " + (playerIndex + 1) + " : " + victoryPoint;
        }
        else
        {
            textScore.text = victoryPoint + " : " + "Player " + (playerIndex + 1);
        }
    }
}
