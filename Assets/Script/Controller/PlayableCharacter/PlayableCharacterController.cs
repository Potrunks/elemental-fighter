using Assets.Script.Business;
using Assets.Script.Business.Implementation;
using Assets.Script.Business.Interface;
using Assets.Script.Entities;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayableCharacterController : MonoBehaviour
{
    [Header("Ground State")]
    GameObject groundCheck;
    public LayerMask groundLayer;
    public float groundCheckRadius;
    bool isGrounding;

    [Header("Playable Character Entity")]
    public PlayableCharacterEntity playableCharacter;

    [Header("Animation")]
    public Animator playableCharacterAnimator;

    Vector2 inputMoveValue;
    bool isDeviceUsed;

    public IPlayableCharacterStateV2 currentState;
    IPlayableCharacterStateV2 nextState;

    IPlayerBusiness playerBusiness;
    IInputDeviceBusiness inputDeviceBusiness;

    private void FixedUpdate()
    {
        isGrounding = groundCheck.isTouchingLayer(groundCheckRadius, groundLayer);
        playerBusiness.ExecuteCheckingPlayableCharacterState(currentState, nextState, this);
    }

    private void Awake()
    {
        playerBusiness = new PlayerBusiness();
        inputDeviceBusiness = new InputDeviceBusiness();
        isDeviceUsed = false;
        groundCheck = transform.Find("GroundCheck").gameObject;
        playableCharacterAnimator = gameObject.GetComponent<Animator>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.transform.position, groundCheckRadius);
    }

    public void OnInputMove(InputAction.CallbackContext context)
    {
        isDeviceUsed = inputDeviceBusiness.CheckPlayerUsingDevice(context);
        inputMoveValue = context.ReadValue<Vector2>();
    }
}
