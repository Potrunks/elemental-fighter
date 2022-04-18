using UnityEngine;
using TMPro;

public class ScorePlayer : MonoBehaviour
{
    private TextMeshProUGUI textScore;
    public int playerIndex;
    public int victoryPoint;
    // Start is called before the first frame update
    void Start()
    {
        textScore = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
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
