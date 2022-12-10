using Assets.Script.Business;
using Assets.Script.Entities;
using Assets.Script.FiniteStateMachine;
using UnityEngine;

public class PowerController : MonoBehaviour
{
    [Header("Elemental Parameter")]
    public PowerEntity _powerEntity;
    public float _selfDestructTimer;

    [Header("Caster Parameter")]
    public PlayableCharacterController _caster;

    [Header("Component")]
    public Transform _spawnPoint;
    public Rigidbody2D _rigidbody;
    public Animator _animator;
    public Collider2D _collider;
    [SerializeField]
    private SpriteRenderer _spriteRenderer;

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
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _willBeDestroyed = false;
        _destroyLimitTimer = Time.time + _selfDestructTimer;
        _spriteRenderer.ChangeColorByIndexPlayer(_caster._playerIndex);
    }

    private void FixedUpdate()
    {
        _willBeDestroyed = this.IsTimeToBeDestruct();
    }

    private void Update()
    {
        _elementalBusiness.CheckElementalStateChange(this);
    }
    #endregion
}
