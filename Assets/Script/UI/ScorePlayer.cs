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
        textScore.text = "Player " + (playerIndex + 1) + " : " + victoryPoint;
    }

    public void Suicide()
    {
        this.victoryPoint = this.victoryPoint - 2;
        DisplayScore();
    }
}
