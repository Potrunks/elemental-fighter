using UnityEngine;
using UnityEngine.InputSystem;

public class MovePlayer : MonoBehaviour
{
    // variable de la vitesse de deplacement
    public float normalMoveSpeed;
    public float stopMoveSpeed;
    public float moveSpeed;
    // fait ref au rigid body du joueur
    public Rigidbody2D rb;
    private Vector3 velocity = Vector3.zero;
    public bool isGrounding;
    public bool isHurting = false;
    public float jumpForce;
    public Transform groundCheck;
    public float groundCheckRadius;
    public Animator animator;
    public LayerMask collisionLayer;
    // Pour la hit box de l'arme au cac (hitBoxWeapon, enemyLayer, hitBoxWeaponRange)
    public GameObject hitBoxWeapon;
    public LayerMask enemyLayer;
    public float hitBoxWeaponRange;
    public int attackDamage;
    // atkRate is to define number of atk per atkRecovery (sec)
    public SpriteRenderer spriteRenderer;
    public GameObject mediumFireElementalPrefabs;
    public GameObject heavyFireElementalPrefabs;
    public GameObject elementalSpawnPoint;
    private float horizontalMovement;
    public bool isBlockingAttack = false;
    public GameObject enemy = null;
    private Vector2 horizontalMovementV2;
    public float dashForce;
    public const float dashCooldownTime = 1;
    public float nextTimeDash;
    public int playerIndex;
    public GameObject dashEffectPrefab;
    public GameObject dashSpawnPoint;
    private ICharacterState currentState;
    private ICharacterState nextState;
    public ParticleSystem dashEffect;
    public ParticleSystem bloodEffect;
    public AudioManager audioManager;
    public float mainAmplifyValue;
    public bool isModeAI = false;

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
        }
        else if (this.gameObject.name == "Character2" && enemy == null)
        {
            enemy = GameObject.Find("Character1");
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

    public float? CalculateDistancePlayerEnemy()
    {
        float distance = Vector3.Distance(this.gameObject.transform.position, enemy.transform.position);
        return distance;
    }
}