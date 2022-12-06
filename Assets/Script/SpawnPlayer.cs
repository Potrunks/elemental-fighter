using Assets.Script.Business;
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
    private PlayableCharacterController _playableCharacterControllerSelected;

    private IPlayerBusiness playerBusiness = new PlayerBusiness();

    void Start()
    {
        selectedCharacter = playerBusiness.GetCharacterSelectedByIndex(playerIndex, GameManager.instance.deviceAndCharacterPlayerByIndex);
        if (selectedCharacter != null)
        {
            _playableCharacterControllerSelected = selectedCharacter.GetComponent<PlayableCharacterController>();
        }
        if (selectedCharacter == null)
        {
            Destroy(scorePlayer);
            Destroy(this.gameObject);
            return;
        }
        _playableCharacterControllerSelected._playerIndex = playerIndex;
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
                    Debug.Log("Activation of AI mode for player " + (_playableCharacterControllerSelected._playerIndex + 1));
                    GetComponentInChildren<EnemyAI>().enabled = true;
                }
                else
                {
                    Debug.Log("Activation of Player mode for player " + (_playableCharacterControllerSelected._playerIndex + 1));
                    playerInput.enabled = true;
                    PauseMenu.instance.inputDeviceByPlayerIndex.Add(playerIndex, playerInput.devices[0]);
                }
                playersIsActivated = true;
            }
        }
    }
}
