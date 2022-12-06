using Assets.Script.Business;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    ICharacterBusiness _characterBusiness;

    private void Awake()
    {
        _characterBusiness = new CharacterBusiness();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayableCharacterController playerFell = other.GetComponent<PlayableCharacterController>();
        if (playerFell != null)
        {
            playerFell.UpdateScoreAfterFell();
            if (playerFell._enemy != null && playerFell._enemy._scorePlayer.victoryPoint == GameManager.instance.victoryPointCondition)
            {
                GameManager.instance.DisplayEndgameResults();
            }
            else
            {
                _characterBusiness.RespawnPlayer(playerFell);
            }
        }
    }
}
