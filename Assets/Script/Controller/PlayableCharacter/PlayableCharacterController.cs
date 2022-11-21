using Assets.Script.Business;
using Assets.Script.Business.Implementation;
using Assets.Script.Business.Interface;
using Assets.Script.Data;
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

    [Header("Move Parameter")]
    public Vector2 inputMoveValue;
    public Rigidbody2D playableCharacterRigidbody;
    float playableCharacterMoveSpeed;
    public bool isDeviceUsed;

    public IPlayableCharacterStateV2 currentState;
    IPlayableCharacterStateV2 nextState;

    IPlayerBusiness playerBusiness;
    IInputDeviceBusiness inputDeviceBusiness;
    ICharacterBusiness characterBusiness;

    private void FixedUpdate()
    {
        isGrounding = groundCheck.isTouchingLayer(groundCheckRadius, groundLayer);
        playableCharacterRigidbody.velocity = characterBusiness.MoveCharacter(inputMoveValue, playableCharacterMoveSpeed, playableCharacterRigidbody, GamePlayValueReference.smoothTimeDuringMove);
        playerBusiness.ExecuteCheckingPlayableCharacterState(currentState, nextState, this);
    }

    private void Awake()
    {
        playerBusiness = new PlayerBusiness();
        inputDeviceBusiness = new InputDeviceBusiness();
        characterBusiness = new CharacterBusiness();

        isDeviceUsed = GamePlayValueReference.startDeviceUsingState;
        playableCharacterMoveSpeed = playableCharacter.moveSpeed;

        groundCheck = transform.Find("GroundCheck").gameObject;
        playableCharacterAnimator = gameObject.GetComponent<Animator>();
        playableCharacterRigidbody = gameObject.GetComponent<Rigidbody2D>();
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
