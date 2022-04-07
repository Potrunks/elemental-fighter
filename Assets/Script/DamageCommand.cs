using UnityEngine;

public class DamageCommand : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public GameObject blockingEffectPrefab;
    public GameObject BlockingEffectSpawnPoint;
    public float blockPush;
    public float heavyATKPushForce;
    void Start()
    {
        currentHealth = maxHealth;
    }
    public void TakeDamage(int damage)
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
    }
    public void DisplayBlockingEffect()
    {
        Instantiate(blockingEffectPrefab, BlockingEffectSpawnPoint.transform.position, Quaternion.identity);
    }

    public void TakeHeavyDamage(){
        if (GetComponent<MovePlayer>().spriteRenderer.flipX == false)
        {
            GetComponent<MovePlayer>().rb.velocity = Vector2.left * heavyATKPushForce;
        }
        else
        {
            GetComponent<MovePlayer>().rb.velocity = Vector2.right * heavyATKPushForce;
        }
    }
}
