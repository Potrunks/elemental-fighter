using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    public int playerIndex;
    public GameObject selectedCharacter;
    public GameObject scorePlayer;
    void Start()
    {
        scorePlayer.GetComponent<ScorePlayer>().playerIndex = playerIndex;
        scorePlayer.GetComponent<ScorePlayer>().victoryPoint = 0;
        // lui donner l'index
        selectedCharacter.GetComponent<MovePlayer>().playerIndex = playerIndex;
        // instancier un joueur et le faire apparaitre sur le spawn
        Instantiate(selectedCharacter, this.transform.position, Quaternion.identity, this.transform);
    }
}
