using UnityEngine;
using UnityEngine.InputSystem;

public class MovePlayer : MonoBehaviour
{
    [Header("Move")]
    // variable de la vitesse de deplacement
    public float normalMoveSpeed;
    public float stopMoveSpeed;
    public float moveSpeed;
    public float jumpForce;
    private float horizontalMovement;
    public Vector2 horizontalMovementV2;
    public float dashForce;
    public const float dashCooldownTime = 1;
    public float nextTimeDash;
    private Vector3 velocity = Vector3.zero;

    [Header("Component")]
    // fait ref au rigid body du joueur
    public Rigidbody2D rb;
    public Transform groundCheck;
    public Animator animator;
    public LayerMask collisionLayer;
    // Pour la hit box de l'arme au cac (hitBoxWeapon, enemyLayer, hitBoxWeaponRange)
    public GameObject hitBoxWeapon;
    public LayerMask enemyLayer;
    public SpriteRenderer spriteRenderer;
    public GameObject mediumFireElementalPrefabs;
    public GameObject heavyFireElementalPrefabs;
    public GameObject elementalSpawnPoint;
    public GameObject enemy = null;
    public MovePlayer enemyMovePlayer = null;
    public DamageCommand enemyDamageCommand = null;
    public GameObject dashEffectPrefab;
    public GameObject dashSpawnPoint;
    public ParticleSystem dashEffect;
    public ParticleSystem bloodEffect;
    public AudioManager audioManager;
    private Renderer rendererCharacter;

    [Header("State")]
    public bool isHurting = false;
    public bool isGrounding;
    public bool isBlockingAttack = false;
    public int playerIndex;
    public ICharacterState currentState;
    private ICharacterState nextState;

    [Header("Checking Condition")]
    public float groundCheckRadius;
    public float hitBoxWeaponRange;

    [Header("Fighting Condition")]
    public int attackDamage;

    [Header("Sound")]
    public float mainAmplifyValue;

    private void Awake()
    {
        foreach (Sound sound in audioManager.sounds)
        {
            sound.amplifyValue = mainAmplifyValue;
        }
    }
    public int GetPlayerIndex()
    {
        return playerIndex;
    }
    private void FixedUpdate()
    {
        if (this.gameObject.name == "Character1" && enemy == null)
        {
            enemy = GameObject.Find("Character2");
            enemyMovePlayer = enemy.GetComponent<MovePlayer>();
            enemyDamageCommand = enemy.GetComponent<DamageCommand>();
        }
        else if (this.gameObject.name == "Character2" && enemy == null)
        {
            enemy = GameObject.Find("Character1");
            enemyMovePlayer = enemy.GetComponent<MovePlayer>();
            enemyDamageCommand = enemy.GetComponent<DamageCommand>();
        }
        isGrounding = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, collisionLayer);
        horizontalMovement = horizontalMovementV2.x * moveSpeed * Time.deltaTime;
        PlayerMove(horizontalMovement);
        Flip();
        nextState = currentState.CheckingStateModification(this);
        if (nextState != null)
        {
            currentState.OnExit(this);
            currentState = nextState;
            currentState.OnEnter(this);
        }
    }
    public void PlayerMove(float _horizontalMovement)
    {
        // le joueur ne pourra pas bouger en fonction de l'axe y a cause du rigidbody.velocity.y
        Vector3 targetVelocity = new Vector2(_horizontalMovement, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, 0.05f);
    }
    private void OnDrawGizmos()
    {
        // permet de voir le cercle en rouge pour savoir si on est au sol
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        Gizmos.DrawWireSphere(hitBoxWeapon.transform.position, hitBoxWeaponRange);
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        if (PauseMenu.instance.isPaused == false)
        {
            // creer un Vector 2 avec toutes les valeur de x et y en fonction des touches appuyé sur le GamePad
            horizontalMovementV2 = context.ReadValue<Vector2>();
        }
    }
    public void OnBlock(InputAction.CallbackContext context)
    {
        if (context.performed && PauseMenu.instance.isPaused == false)
        {
            isBlockingAttack = true;
            currentState.PerformingInput("Block");
        }
        else
        {
            isBlockingAttack = false;
        }
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && PauseMenu.instance.isPaused == false)
        {
            currentState.PerformingInput("Jumping");
        }
    }
    public void HitDamage(float hitBoxWeaponRange)
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(hitBoxWeapon.transform.position, hitBoxWeaponRange, enemyLayer);
        foreach (Collider2D enemyCollider2D in hitEnemies)
        {
            enemyCollider2D.GetComponent<DamageCommand>().TakeDamage(attackDamage);
        }
    }
    public void Flip()
    {
        if (transform.position.x < enemy.transform.position.x)
        {
            if (spriteRenderer.flipX == false)
            {
                return;
            }
            else
            {
                spriteRenderer.flipX = false;
                elementalSpawnPoint.GetComponent<ElementalSpawnPoint>().Rotate(spriteRenderer.flipX);
                hitBoxWeapon.GetComponent<HitBoxWeapon>().Rotate(spriteRenderer.flipX);
                dashSpawnPoint.GetComponent<DashSpawnPointFlip>().Rotate(spriteRenderer.flipX);
                dashEffect.transform.Rotate(0f, 180f, 0f);
                bloodEffect.transform.Rotate(0f, 0f, 180f);
                GetComponent<DamageCommand>().bleedingEffect.transform.Rotate(0f, 180f, 0f);
            }
        }
        else if (transform.position.x > enemy.transform.position.x)
        {
            if (spriteRenderer.flipX == true)
            {
                return;
            }
            else
            {
                spriteRenderer.flipX = true;
                elementalSpawnPoint.GetComponent<ElementalSpawnPoint>().Rotate(spriteRenderer.flipX);
                hitBoxWeapon.GetComponent<HitBoxWeapon>().Rotate(spriteRenderer.flipX);
                dashSpawnPoint.GetComponent<DashSpawnPointFlip>().Rotate(spriteRenderer.flipX);
                dashEffect.transform.Rotate(0f, 180f, 0f);
                bloodEffect.transform.Rotate(0f, 0f, 180f);
                GetComponent<DamageCommand>().bleedingEffect.transform.Rotate(0f, 180f, 0f);
            }
        }
    }
    public void OnLightATK(InputAction.CallbackContext context)
    {
        if (context.performed && PauseMenu.instance.isPaused == false)
        {
            currentState.PerformingInput("LightATK");
        }
    }
    void Start()
    {
        animator = GetComponent<Animator>();
        if (GetComponent<MovePlayer>().playerIndex == 0)
        {
            //this.gameObject.tag = "Player";
            this.gameObject.name = "Character1";
        }
        else
        {
            //this.gameObject.tag = "Player2";
            this.gameObject.name = "Character2";
        }
        currentState = new IdleCharacterState();
        currentState.OnEnter(this);
        MultipleTargetCamFollow.instance.players.Add(this.transform);
        rendererCharacter = GetComponent<Renderer>();
        SetRendererColorByPlayerIndex(playerIndex);
    }

    public void OnMediumATK(InputAction.CallbackContext context)
    {
        if (context.performed && PauseMenu.instance.isPaused == false)
        {
            currentState.PerformingInput("MediumATK");
        }
    }
    public void OnHeavyATK(InputAction.CallbackContext context)
    {
        if (context.performed && PauseMenu.instance.isPaused == false)
        {
            currentState.PerformingInput("HeavyATK");
        }
    }
    private void CastMediumFireElemental()
    {
        Instantiate(mediumFireElementalPrefabs, elementalSpawnPoint.transform.position, elementalSpawnPoint.transform.rotation, this.transform);
    }

    private void CastHeavyFireElemental()
    {
        Instantiate(heavyFireElementalPrefabs, elementalSpawnPoint.transform.position, elementalSpawnPoint.transform.rotation, this.transform);
    }

    public void OnDashMove(InputAction.CallbackContext context)
    {
        if (context.performed && Time.time > nextTimeDash && PauseMenu.instance.isPaused == false)
        {
            if ((spriteRenderer.flipX == true && rb.velocity.x < -0.1f)
            || (spriteRenderer.flipX == false && rb.velocity.x > 0.1f))
            {
                currentState.PerformingInput("Dash");
            }
        }
    }
    public void DashEffect()
    {
        dashEffect.Play();
        //Instantiate(dashEffectPrefab, dashSpawnPoint.transform.position, Quaternion.identity);
    }

    public void BloodEffect()
    {
        bloodEffect.Play();
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            PauseMenu.instance.PauseGame(this.playerIndex);
        }
    }

    public void SetRendererColorByPlayerIndex(int playerIndex)
    {
        if (playerIndex == 1)
        {
            // Blue
            rendererCharacter.material.color = new Color32(133, 136, 253, 255);
        }
        if (playerIndex == 2)
        {
            // Green
            rendererCharacter.material.color = new Color32(141, 253, 134, 255);
        }
        if (playerIndex == 3)
        {
            // Yellow
            rendererCharacter.material.color = new Color32(245, 253, 133, 255);
        }
    }
}