using Assets.Script.Business;
using Assets.Script.Business.Implementation;
using Assets.Script.Business.Interface;
using Assets.Script.Entities;
using UnityEngine;

public class PlayableCharacterController : MonoBehaviour
{
    [Header("Ground State")]
    GameObject groundCheck;
    public LayerMask groundLayer;
    public float groundCheckRadius;
    bool isGrounding;

    [Header("Playable Character Entity")]
    public PlayableCharacterEntity playableCharacter;

    public IPlayableCharacterStateV2 currentState;
    IPlayableCharacterStateV2 nextState;

    IPlayerBusiness playerBusiness;

    private void FixedUpdate()
    {
        isGrounding = groundCheck.isTouchingLayer(groundCheckRadius, groundLayer);

        playerBusiness.ExecuteCheckingPlayableCharacterState(currentState, nextState, this);
    }

    private void Awake()
    {
        playerBusiness = new PlayerBusiness();
        groundCheck = this.transform.Find("GroundCheck").gameObject;
    }
}
