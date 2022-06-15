using UnityEngine;

public class HeavyFireElementalCommand : MonoBehaviour
{
    public float speed;
    public int heavyFireElementalDamage;
    public Rigidbody2D rb;
    public GameObject MediumFireElementalImpactEffect;
    public GameObject target;
    public MovePlayer caster;
    public Transform elementalSpawnPointTransform;

    void Start()
    {
        rb.AddForce(elementalSpawnPointTransform.right * speed, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageCommand targetHit = other.GetComponent<DamageCommand>();
        Transform targetTransform = other.GetComponent<Transform>();
        MovePlayer targetMoveplayer = other.GetComponent<MovePlayer>();
        try
        {
            if (targetHit.isInvincible == false)
            {
                caster.SetPlayerAsEnemy(targetMoveplayer);
                targetHit.SetIsAttackedFromBehind(targetMoveplayer, targetTransform, this.gameObject.transform);
                targetMoveplayer.isHurtingByPushAttack = true;
                targetHit.TakeDamage(heavyFireElementalDamage);
            }
        }
        catch (System.NullReferenceException e)
        {
            Debug.Log("The projectile of " + this.gameObject.name + " doesn't touch an enemy character" + e);
        }
        finally
        {
            Destroy(this.gameObject);
            Instantiate(MediumFireElementalImpactEffect, transform.position, transform.rotation);
        }
    }
}
