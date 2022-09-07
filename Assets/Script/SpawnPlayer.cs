using Assets.Script.Business.Implementation;
using Assets.Script.Business.Interface;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnPlayer : MonoBehaviour
{
    public int playerIndex;
    private GameObject selectedCharacter;
    public GameObject scorePlayer;
    private ScorePlayer scorePlayerScript;
    private bool playersIsActivated = false;
    private PlayerInput playerInput;

    private IPlayerBusiness playerBusiness = new PlayerBusiness();

    void Start()
    {
        selectedCharacter = playerBusiness.GetCharacterSelectedByIndex(playerIndex, GameManager.instance.deviceAndCharacterPlayerByIndex);
        if (selectedCharacter == null)
        {
            Destroy(scorePlayer);
            Destroy(this.gameObject);
            return;
        }
        selectedCharacter.GetComponent<MovePlayer>().playerIndex = playerIndex;
        Instantiate(selectedCharacter, this.transform.position, Quaternion.identity, this.transform);
        scorePlayerScript = scorePlayer.GetComponent<ScorePlayer>();
        scorePlayerScript.playerIndex = playerIndex;
        scorePlayerScript.victoryPoint = 0;
        playerInput = GetComponentInChildren<PlayerInput>();
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
                    playerInput.enabled = true;
                    PauseMenu.instance.inputDeviceByPlayerIndex.Add(playerIndex, playerInput.devices[0]);
                }
                playersIsActivated = true;
            }
        }
    }
}
