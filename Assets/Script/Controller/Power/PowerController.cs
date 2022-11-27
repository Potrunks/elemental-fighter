using Assets.Script.Business;
using Assets.Script.Business.Implementation;
using Assets.Script.Business.Interface;
using Assets.Script.Entities;
using Assets.Script.FiniteStateMachine;
using UnityEngine;

public class PowerController : MonoBehaviour
{
    [Header("Elemental Parameter")]
    public PowerEntity _powerEntity;
    public float _selfDestructTimer;

    [Header("Caster Parameter")]
    public MovePlayer _caster;
    public PlayableCharacterController _casterV2;

    [Header("Component")]
    public Transform _spawnPoint;
    public Rigidbody2D _rigidbody;
    public Animator _animator;
    public Collider2D _collider;

    [Header("InGame Value")]
    public bool _willBeDestroyed;
    public float _destroyLimitTimer;

    public IElementalBusiness _elementalBusiness;
    public ICharacterBusiness _characterBusiness;

    public IPowerState currentState;
    public IPowerState nextState;

    #region MonoBehaviour Method
    private void Awake()
    {
        _elementalBusiness = new ElementalBusiness();
        _characterBusiness = new CharacterBusiness();

        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();

        _willBeDestroyed = false;
        _destroyLimitTimer = Time.time + _selfDestructTimer;
    }

    private void FixedUpdate()
    {
        _willBeDestroyed = this.isTimeToBeDestruct();
    }

    private void Update()
    {
        _elementalBusiness.CheckElementalStateChange(this);
    }
    #endregion
}
