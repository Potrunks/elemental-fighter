using UnityEngine;

public class HeavyFireElementalCommand : MonoBehaviour
{
    public float speed;
    public int heavyFireElementalDamage;
    public Rigidbody2D rb;
    public GameObject MediumFireElementalImpactEffect;
    public GameObject target;
    public MovePlayer caster;
    public float heavyATKPushForce;
    void Start()
    {
        caster = GetComponentInParent<MovePlayer>();
        if (caster.GetPlayerIndex() == 0)
        {
            target = GameObject.Find("Character2");
        }
        else
        {
            target = GameObject.Find("Character1");
        }
        transform.right = target.transform.position - transform.position;
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        try
        {
            other.GetComponent<DamageCommand>().TakeDamage(heavyFireElementalDamage);
            //other.GetComponent<DamageCommand>().TakeHeavyDamage();
            EnemyTakeHeavyATK(other.GetComponent<Rigidbody2D>(), other.GetComponent<MovePlayer>());
        }
        finally
        {
            Destroy(this.gameObject);
            Instantiate(MediumFireElementalImpactEffect, transform.position, transform.rotation);
        }
    }

    private void EnemyTakeHeavyATK(Rigidbody2D rb, MovePlayer player)
    {
        if (player.spriteRenderer.flipX == false)
        {
            rb.velocity = Vector2.left * heavyATKPushForce;
        }
        else
        {
            rb.velocity = Vector2.right * heavyATKPushForce;
        }
    }
}
