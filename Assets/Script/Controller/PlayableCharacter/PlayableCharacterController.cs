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
    public LayerMask groundLayer;
    public float groundCheckRadius;
    [SerializeField]
    bool isGrounding;
    public GameObject groundCheck;

    [Header("Playable Character Entity")]
    public PlayableCharacterEntity playableCharacter;

    [Header("Animation")]
    public Animator playableCharacterAnimator;

    [Header("Move Parameter")]
    [SerializeField]
    Vector2 inputMoveValue;
    public Rigidbody2D playableCharacterRigidbody;
    [SerializeField]
    float playableCharacterMoveSpeed;
    public bool isDeviceUsed;
    [SerializeField]
    bool isLeftFlip;

    [Header("Elemental Spawn")]
    [SerializeField]
    GameObject gameObjectElementalSpawnPoint;

    public IPlayableCharacterStateV2 currentState;
    IPlayableCharacterStateV2 nextState;

    IPlayerBusiness playerBusiness;
    IInputDeviceBusiness inputDeviceBusiness;
    ICharacterBusiness characterBusiness;

    private void FixedUpdate()
    {
        isGrounding = groundCheck.isTouchingLayer(groundCheckRadius, groundLayer);
        playableCharacterRigidbody.velocity = characterBusiness.MoveCharacter(inputMoveValue, playableCharacterMoveSpeed, playableCharacterRigidbody, GamePlayValueReference.smoothTimeDuringMove);
        if (isDeviceUsed)
        {
            if (playableCharacterRigidbody.velocity.x < GamePlayValueReference.velocityXLowThreshold
                && !isLeftFlip)
            {
                transform.Rotate(0f, 180f, 0f);
                isLeftFlip = true;
            }
            if (playableCharacterRigidbody.velocity.x > GamePlayValueReference.velocityXHighThreshold
                && isLeftFlip)
            {
                transform.Rotate(0f, 180f, 0f);
                isLeftFlip = false;
            }
        }
        gameObjectElementalSpawnPoint.transform.rotation = playerBusiness.CalculateShootAngle(inputMoveValue);
        nextState = currentState.CheckingStateModification(this);
        if (nextState != null)
        {
            currentState.OnExit(this);
            currentState = nextState;
            currentState.OnEnter(this);
        }
    }

    private void Awake()
    {
        playerBusiness = new PlayerBusiness();
        inputDeviceBusiness = new InputDeviceBusiness();
        characterBusiness = new CharacterBusiness();

        isDeviceUsed = GamePlayValueReference.startDeviceUsingState;
        playableCharacterMoveSpeed = playableCharacter.moveSpeed;
        isLeftFlip = false;

        gameObjectElementalSpawnPoint = transform.Find("ElementalSpawnPoint").gameObject;
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
        isDeviceUsed = inputDeviceBusiness.CheckPlayerUsingDevice(context, isDeviceUsed);
        inputMoveValue = context.ReadValue<Vector2>();
    }
}
