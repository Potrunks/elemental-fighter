using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public float volumeMainTheme;
    public int victoryPointCondition;
    public int timeCondition;
    public bool timeIsActivated;
    public float currentTime;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (currentTime > 0 && timeIsActivated)
        {
            currentTime -= Time.deltaTime % 60;
            if (currentTime <= 0 && currentTime != -1)
            {
                DisplayEndgameResults();
            }
        }
    }

    // Display an end game results when one player have enough kill or the time is over
    public void DisplayEndgameResults()
    {
        Debug.Log("Displaying endgame results...");
        ScorePlayer[] scorePlayerArray = GameObject.FindObjectsOfType<ScorePlayer>();
        if (scorePlayerArray == null || scorePlayerArray.Length == 0)
        {
            Debug.LogWarning("No component type of ScorePlayer found");
            return;
        }
        List<ScorePlayerResult> scorePlayerResultList = new List<ScorePlayerResult>();
        foreach (ScorePlayer scorePlayer in scorePlayerArray)
        {
            ScorePlayerResult scorePlayerResult = new ScorePlayerResult();
            scorePlayerResult.kill = scorePlayer.victoryPoint;
            scorePlayerResult.name = "Player " + (scorePlayer.playerIndex + 1);
            scorePlayerResultList.Add(scorePlayerResult);
        }
        scorePlayerResultList.Sort((scorePlayerResult1, scorePlayerResult2) => scorePlayerResult2.kill.CompareTo(scorePlayerResult1.kill));
        TextMeshProUGUI scoreListText = PauseMenu.instance.scoreList.GetComponent<TextMeshProUGUI>();
        scoreListText.text = "";
        for (int i = 0; i < scorePlayerResultList.Count; i++)
        {
            scoreListText.text += scorePlayerResultList[i].name + " : " + scorePlayerResultList[i].kill + " pts";
            if (i < scorePlayerResultList.Count - 1)
            {
                scoreListText.text += "\n";
            }
        }
        TextMeshProUGUI winnerText = PauseMenu.instance.winner.GetComponent<TextMeshProUGUI>();
        winnerText.text = scorePlayerResultList[0].name + " win";
        PauseMenu.instance.EndTheGame();
    }
}
