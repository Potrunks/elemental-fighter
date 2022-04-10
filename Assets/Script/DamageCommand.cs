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
        ResetHealth();
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

    public void ResetHealth(){
        currentHealth = maxHealth;
    }

    public void ResetPlayer(MovePlayer player, GameObject enemy){
        ResetHealth();
        PlayerStartSpawn(player);
        EnemyStartSpawn(enemy);
    }

    public void PlayerStartSpawn(MovePlayer player){
        player.transform.position = player.GetComponentInParent<SpawnPlayer>().transform.position;
    }

    public void EnemyStartSpawn(GameObject enemy){
        enemy.transform.position = enemy.GetComponentInParent<SpawnPlayer>().transform.position;
    }
}
