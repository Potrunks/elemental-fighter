using UnityEngine;
using UnityEngine.UI;

public class ScorePlayer : MonoBehaviour
{
    private Text textScore;
    public int playerIndex;
    public int victoryPoint;
    // Start is called before the first frame update
    void Start()
    {
        textScore = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        textScore.text = "Player " + (playerIndex + 1) + " : " + victoryPoint;
    }
}
