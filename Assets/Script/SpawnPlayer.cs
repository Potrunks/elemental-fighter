using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnPlayer : MonoBehaviour
{
    public int playerIndex;
    public GameObject selectedCharacter;
    public GameObject scorePlayer;
    private ScorePlayer scorePlayerScript;
    private bool playersIsActivated = false;

    void Start()
    {
        scorePlayerScript = scorePlayer.GetComponent<ScorePlayer>();
        scorePlayerScript.playerIndex = playerIndex;
        scorePlayerScript.victoryPoint = 0;
        selectedCharacter.GetComponent<MovePlayer>().playerIndex = playerIndex;
        Instantiate(selectedCharacter, this.transform.position, Quaternion.identity, this.transform);
    }

    private void Update()
    {
        if (playersIsActivated == false)
        {
            if (ReadyFightScript.instance.fightIsStarted)
            {
                if (GameManager.instance.selectedMode[playerIndex])
                {
                    Debug.Log("Activation of AI mode for player " + (GetComponentInChildren<MovePlayer>().playerIndex + 1));
                    GetComponentInChildren<EnemyAI>().enabled = true;
                }
                else
                {
                    Debug.Log("Activation of Player mode for player " + (GetComponentInChildren<MovePlayer>().playerIndex + 1));
                    GetComponentInChildren<PlayerInput>().enabled = true;
                }
                playersIsActivated = true;
            }
        }
    }
}
