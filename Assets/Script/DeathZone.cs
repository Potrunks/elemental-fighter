using Assets.Script.Business;
using Assets.Script.Business.Implementation;
using Assets.Script.Business.Interface;
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
            playerFell.UpdateScoreAfterDeath();
            if (playerFell._lastTouchedBy._scorePlayer.victoryPoint == GameManager.instance.victoryPointCondition)
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
