using Assets.Script.Business;
using Assets.Script.Business.Implementation;
using Assets.Script.Business.Interface;
using Assets.Script.Data;
using Assets.Script.Data.Reference;
using Assets.Script.Entities;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayableCharacterController : MonoBehaviour
{
    [Header("Ground State")]
    public LayerMask groundLayer;
    public float groundCheckRadius;
    public bool isGrounding;
    public GameObject groundCheck;

    [Header("Playable Character Parameter")]
    public PlayableCharacterEntity playableCharacter;
    public IDictionary<PowerLevelReference, GameObject> kvpPowerModelByPowerLevel;
    public int _currentHealth;

    [Header("Model Component")]
    public Animator playableCharacterAnimator;
    public SpriteRenderer _spriteRenderer;

    [Header("Move Parameter")]
    [SerializeField]
    Vector2 inputMoveValue;
    public Rigidbody2D playableCharacterRigidbody;
    public float playableCharacterMoveSpeed;
    public bool isDeviceUsed;
    public bool isLeftFlip;

    [Header("Elemental Spawn")]
    public GameObject gameObjectElementalSpawnPoint;

    [Header("Hit Box")]
    public GameObject _hitBoxAtk;
    public float _hitBoxAtkRadius;

    [Header("InGame Data")]
    public int _playerIndex;
    public PlayableCharacterController _lastTouchedBy;
    public bool _isTouchingByAttack;
    public SpawnPlayer _spawnPlayerPoint;
    public ScorePlayer _scorePlayer;

    [Header("Invincible State")]
    public bool _isInvincible;
    public float _invincibleLimitTimer;

    public IPlayableCharacterStateV2 currentState;
    public IPlayableCharacterStateV2 nextState;

    IPlayerBusiness playerBusiness;
    IInputDeviceBusiness inputDeviceBusiness;
    public ICharacterBusiness characterBusiness;
    public IElementalBusiness elementalBusiness;
    public IPhysicsBusiness _physicsBusiness;

    #region MonoBehaviour Method
    private void FixedUpdate()
    {
        isGrounding = groundCheck.isTouchingLayer(groundCheckRadius, groundLayer);
        playableCharacterRigidbody.velocity = characterBusiness.MoveCharacter(inputMoveValue, playableCharacterMoveSpeed, playableCharacterRigidbody, GamePlayValueReference.smoothTimeDuringMove);
        characterBusiness.CheckFlipCharacterModel(this);
        gameObjectElementalSpawnPoint.transform.rotation = playerBusiness.CalculateShootAngle(inputMoveValue, isLeftFlip, isDeviceUsed);
        _isInvincible = this.CheckInvincibleEndTime();
    }

    private void Update()
    {
        characterBusiness.CheckCharacterStateChange(this);
    }

    private void Awake()
    {
        playerBusiness = new PlayerBusiness();
        inputDeviceBusiness = new InputDeviceBusiness();
        characterBusiness = new CharacterBusiness();
        elementalBusiness = new ElementalBusiness();
        _physicsBusiness = new PhysicsBusiness();

        isDeviceUsed = GamePlayValueReference.startDeviceUsingState;
        playableCharacterMoveSpeed = playableCharacter.MoveSpeed;
        isLeftFlip = false;
        _isTouchingByAttack = false;
        _isInvincible = false;
        kvpPowerModelByPowerLevel = playableCharacter.PowerEntityList.ToDictionary(pow => pow.powerLevel, pow => pow.powerModel);
        _currentHealth = playableCharacter.MaxHealth;

        gameObjectElementalSpawnPoint = transform.Find("ElementalSpawnPoint").gameObject;
        _hitBoxAtk = transform.Find("HitBoxAtk").gameObject;
        playableCharacterAnimator = gameObject.GetComponent<Animator>();
        playableCharacterRigidbody = gameObject.GetComponent<Rigidbody2D>();
        _spawnPlayerPoint = GetComponentInParent<SpawnPlayer>();
        _scorePlayer = _spawnPlayerPoint.scorePlayer.GetComponent<ScorePlayer>();
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
    #endregion

    #region Gizmos
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.transform.position, groundCheckRadius);
        Gizmos.DrawWireSphere(_hitBoxAtk.transform.position, _hitBoxAtkRadius);
    }
    #endregion

    #region Action
    public void OnInputMove(InputAction.CallbackContext context)
    {
        isDeviceUsed = inputDeviceBusiness.CheckPlayerUsingDevice(context, isDeviceUsed);
        inputMoveValue = context.ReadValue<Vector2>();
    }

    public void OnInputJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            currentState.PerformingInput(PlayableCharacterActionReference.Jump);
        }
    }

    public void OnInputMediumAtk(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            currentState.PerformingInput(PlayableCharacterActionReference.MediumAtk);
        }
    }

    public void OnInputLightAtk(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            currentState.PerformingInput(PlayableCharacterActionReference.LightAtk);
        }
    }

    public void OnInputHeavyAtk(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            currentState.PerformingInput(PlayableCharacterActionReference.HeavyAtk);
        }
    }

    public void OnInputSpecialAtk(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            currentState.PerformingInput(PlayableCharacterActionReference.SpecialAtk);
        }
    }

    public void OnInputSpecialAtk2(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            currentState.PerformingInput(PlayableCharacterActionReference.SpecialAtk2);
        }
    }

    public void OnThrowLightAtk()
    {
        characterBusiness.InflictedMeleeDamageAfterHitBoxContact(_hitBoxAtk, _hitBoxAtkRadius, this, isPushingAtk: true);
    }
    #endregion
}
