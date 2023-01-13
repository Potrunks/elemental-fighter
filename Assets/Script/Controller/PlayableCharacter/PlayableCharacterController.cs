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

    [Header("Blood effect component")]
    public ParticleSystem _bloodEffectForDamage;
    public ParticleSystem _bloodEffectForCurrentHealth;
    private float? _nextBleedingTime;

    [Header("Move Parameter")]
    public Vector2 inputMoveValue;
    public Rigidbody2D playableCharacterRigidbody;
    public float playableCharacterMoveSpeed;
    public bool isDeviceUsed;
    public bool _isLeftFlip;

    [Header("Elemental Spawn")]
    public GameObject gameObjectElementalSpawnPoint;

    [Header("Hit Box")]
    public GameObject _hitBoxAtk;
    public float _hitBoxAtkRadius;

    [Header("InGame Data")]
    public int _playerIndex;
    public PlayableCharacterController _enemy;
    public bool _isTouchingByAttack;
    public SpawnPlayer _spawnPlayerPoint;
    public ScorePlayer _scorePlayer;

    [Header("Invincible State")]
    public bool _isInvincible;
    public float _invincibleLimitTimer;

    [Header("Audio")]
    public IDictionary<SoundEffectType, List<AudioSource>> _soundEffectListByType;
    public IDictionary<VoiceType, List<AudioSource>> _voiceListByType;

    public IPlayableCharacterStateV2 currentState;
    public IPlayableCharacterStateV2 nextState;

    IPlayerBusiness playerBusiness;
    IInputDeviceBusiness inputDeviceBusiness;
    public ICharacterBusiness characterBusiness;
    public IElementalBusiness elementalBusiness;
    public IPhysicsBusiness _physicsBusiness;
    public IAudioBusiness _audioBusiness;

    #region MonoBehaviour Method
    private void FixedUpdate()
    {
        playableCharacterRigidbody.velocity = characterBusiness.MoveCharacter(inputMoveValue, playableCharacterMoveSpeed, playableCharacterRigidbody, GamePlayValueReference.smoothTimeDuringMove);
    }

    private void Update()
    {
        isGrounding = groundCheck.isTouchingLayer(groundCheckRadius, groundLayer);
        characterBusiness.CheckFlipCharacterModel(this);
        gameObjectElementalSpawnPoint.transform.rotation = playerBusiness.CalculateShootAngle(inputMoveValue, _isLeftFlip, isDeviceUsed);
        _isInvincible = this.CheckInvincibleEndTime();
        _nextBleedingTime = characterBusiness.DoBleedingEffect(_currentHealth, playableCharacter.MaxHealth, _nextBleedingTime, _bloodEffectForCurrentHealth);
        characterBusiness.CheckCharacterStateChange(this);
    }

    private void Awake()
    {
        playerBusiness = new PlayerBusiness();
        inputDeviceBusiness = new InputDeviceBusiness();
        characterBusiness = new CharacterBusiness();
        elementalBusiness = new ElementalBusiness();
        _physicsBusiness = new PhysicsBusiness();
        _audioBusiness = new AudioBusiness();

        playableCharacterAnimator = gameObject.GetComponent<Animator>();
        playableCharacterRigidbody = gameObject.GetComponent<Rigidbody2D>();
        _spawnPlayerPoint = GetComponentInParent<SpawnPlayer>();
        if (_spawnPlayerPoint != null)
        {
            _scorePlayer = _spawnPlayerPoint.scorePlayer.GetComponent<ScorePlayer>();
        }
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        gameObjectElementalSpawnPoint = transform.Find("ElementalSpawnPoint").gameObject;
        _hitBoxAtk = transform.Find("HitBoxAtk").gameObject;

        isDeviceUsed = GamePlayValueReference.startDeviceUsingState;
        playableCharacterMoveSpeed = playableCharacter.MoveSpeed;
        _isLeftFlip = this.IsFlipLeft();
        _isTouchingByAttack = false;
        _isInvincible = false;
        kvpPowerModelByPowerLevel = playableCharacter.PowerEntityList.ToDictionary(pow => pow.powerLevel, pow => pow.powerModel);
        _currentHealth = playableCharacter.MaxHealth;
        if (MultipleTargetCamFollow.instance != null)
        {
            MultipleTargetCamFollow.instance.players.Add(transform);
        }
        _spriteRenderer.ChangeColorByIndexPlayer(_playerIndex);
        _nextBleedingTime = null;
        _soundEffectListByType = _audioBusiness.CreateAudioSourceListBySoundEffectType(playableCharacter.SoundEffectList, gameObject);
        _voiceListByType = _audioBusiness.CreateAudioSourceListByVoiceType(playableCharacter.VoiceList, gameObject);
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
        if (PauseMenu.instance == null || !PauseMenu.instance.isPaused)
        {
            isDeviceUsed = inputDeviceBusiness.CheckPlayerUsingDevice(context, isDeviceUsed);
            inputMoveValue = context.ReadValue<Vector2>();
        }
        
    }

    public void OnInputJump(InputAction.CallbackContext context)
    {
        if (context.started && (PauseMenu.instance == null || !PauseMenu.instance.isPaused))
        {
            currentState.PerformingInput(PlayableCharacterActionReference.Jump);
        }
    }

    public void OnInputMediumAtk(InputAction.CallbackContext context)
    {
        if (context.started && (PauseMenu.instance == null || !PauseMenu.instance.isPaused))
        {
            currentState.PerformingInput(PlayableCharacterActionReference.MediumAtk);
        }
    }

    public void OnInputLightAtk(InputAction.CallbackContext context)
    {
        if (context.started && (PauseMenu.instance == null || !PauseMenu.instance.isPaused))
        {
            currentState.PerformingInput(PlayableCharacterActionReference.LightAtk);
        }
    }

    public void OnInputHeavyAtk(InputAction.CallbackContext context)
    {
        if (context.started && (PauseMenu.instance == null || !PauseMenu.instance.isPaused))
        {
            currentState.PerformingInput(PlayableCharacterActionReference.HeavyAtk);
        }
    }

    public void OnInputSpecialAtk(InputAction.CallbackContext context)
    {
        if (context.started && (PauseMenu.instance == null || !PauseMenu.instance.isPaused))
        {
            currentState.PerformingInput(PlayableCharacterActionReference.SpecialAtk);
        }
    }

    public void OnInputSpecialAtk2(InputAction.CallbackContext context)
    {
        if (context.started && (PauseMenu.instance == null || !PauseMenu.instance.isPaused))
        {
            currentState.PerformingInput(PlayableCharacterActionReference.SpecialAtk2);
        }
    }

    public void OnThrowLightAtk()
    {
        characterBusiness.InflictedMeleeDamageAfterHitBoxContact(_hitBoxAtk, _hitBoxAtkRadius, this, isPushingAtk: true);
    }

    public void CastElementalPower(PowerLevelReference level)
    {
        kvpPowerModelByPowerLevel.TryGetValue(level, out GameObject elementalToCast);
        elementalBusiness.InstantiateElemental(elementalToCast, gameObjectElementalSpawnPoint, this);
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.started && (PauseMenu.instance == null || !PauseMenu.instance.isPaused))
        {
            PauseMenu.instance?.PauseGame(_playerIndex);
        }
    }
    #endregion
}
