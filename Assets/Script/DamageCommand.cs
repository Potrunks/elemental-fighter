using UnityEngine;
using System.Collections;

public class DamageCommand : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    private int percentageHealth = 100;
    public GameObject blockingEffectPrefab;
    public GameObject BlockingEffectSpawnPoint;
    public float blockPush;
    public float heavyATKPushForce;
    public ParticleSystem bleedingEffect;
    private float bleedingCooldownTime = 5f;
    private float nextBleedingTime = 0f;
    private bool isBleeding = false;
    public bool isInvincible = false;
    [SerializeField]
    private int invincibleDuration;

    private void Update()
    {
        Bleeding();
    }

    private void Bleeding()
    {
        if (CheckBleedingCondition())
        {
            bleedingEffect.Play();
            UpdateNextBleeding();
        }
    }

    private bool CheckBleedingCondition()
    {
        if (isBleeding == true && nextBleedingTime < Time.time)
        {
            return true;
        }
        return false;
    }

    private void UpdateNextBleeding()
    {
        nextBleedingTime = Time.time + bleedingCooldownTime;
    }

    void Start()
    {
        ResetHealth();
    }
    public void TakeDamage(int damage)
    {
        if (isInvincible == false)
        {
            if (GetComponent<MovePlayer>().isBlockingAttack == true)
        {
            currentHealth -= damage / 5;
            GetComponent<MovePlayer>().isHurting = true;
        }
        else
        {
            currentHealth -= damage;
            GetComponent<MovePlayer>().isHurting = true;
        }
        UpdateBleedingState();
        }
    }

    private void UpdateBleedingState()
    {
        CalculatePercentageHealth();
        if (isBleeding == false)
        {
            CheckingBleedingStartCondition();
        }
        else
        {
            UpdateBleedingCooldownTime();
        }
    }

    private void CheckingBleedingStartCondition()
    {
        if (percentageHealth <= 75)
        {
            isBleeding = true;
        }
    }

    private void UpdateBleedingCooldownTime()
    {
        if (percentageHealth <= 50 && percentageHealth > 25 && bleedingCooldownTime != 2.5f)
        {
            bleedingCooldownTime = 2.5f;
        }
        else if (percentageHealth <= 25 && percentageHealth > 0 && bleedingCooldownTime != 1.25f)
        {
            bleedingCooldownTime = 1.25f;
        }
    }

    private void CalculatePercentageHealth()
    {
        percentageHealth = (currentHealth * 100) / maxHealth;
    }

    public void DisplayBlockingEffect()
    {
        Instantiate(blockingEffectPrefab, BlockingEffectSpawnPoint.transform.position, Quaternion.identity);
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
    }

    public void ResetPlayer(MovePlayer player, GameObject enemy)
    {
        enemy.GetComponentInParent<SpawnPlayer>().scorePlayer.GetComponent<ScorePlayer>().UpdateScore();
        int enemyScore = enemy.GetComponentInParent<SpawnPlayer>().scorePlayer.GetComponent<ScorePlayer>().victoryPoint;
        if (enemyScore == GameManager.instance.victoryPointCondition)
        {
            Debug.Log("The player " + (enemy.GetComponent<MovePlayer>().playerIndex + 1) + " win the game !!!");
            GameManager.instance.DisplayEndgameResults();
        }
        else
        {
            ResetHealth();
            PlayerStartSpawn(player);
            ResetBleedingState();
        }
    }

    public void PlayerStartSpawn(MovePlayer player)
    {
        player.transform.position = player.GetComponentInParent<SpawnPlayer>().transform.position;
        StartCoroutine(invicibleWhenRespawn(player));
    }

    private void ResetBleedingState()
    {
        percentageHealth = 100;
        bleedingCooldownTime = 5f;
        nextBleedingTime = 0f;
        isBleeding = false;
    }

    private IEnumerator invicibleWhenRespawn(MovePlayer player)
    {
        isInvincible = true;
        player.spriteRenderer.material.color = new Color(player.spriteRenderer.material.color.r, player.spriteRenderer.material.color.g, player.spriteRenderer.material.color.b, 0.25f);
        yield return new WaitForSeconds(invincibleDuration);
        isInvincible = false;
        player.spriteRenderer.material.color = new Color(player.spriteRenderer.material.color.r, player.spriteRenderer.material.color.g, player.spriteRenderer.material.color.b, 1f);
    }
}