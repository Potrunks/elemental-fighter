using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnPlayer : MonoBehaviour
{
    public int playerIndex;
    public GameObject selectedCharacter;
    public GameObject scorePlayer;
    void Start()
    {
        scorePlayer.GetComponent<ScorePlayer>().playerIndex = playerIndex;
        scorePlayer.GetComponent<ScorePlayer>().victoryPoint = 0;
        selectedCharacter.GetComponent<MovePlayer>().playerIndex = playerIndex;
        if (GameManager.instance.selectedMode[playerIndex])
        {
            selectedCharacter.GetComponent<EnemyAI>().enabled = true;
            selectedCharacter.GetComponent<PlayerInput>().enabled = false;
        }
        Instantiate(selectedCharacter, this.transform.position, Quaternion.identity, this.transform);
    }
}
