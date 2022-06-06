using UnityEngine;

public class MediumFireElementalCommand : MonoBehaviour
{
    public float speed;
    public int mediumFireElementalDamage;
    public Rigidbody2D rb;
    public GameObject mediumFireElementalImpactEffect;
    public GameObject target;
    public MovePlayer caster;

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
        DamageCommand targetHit = other.GetComponent<DamageCommand>();
        Transform targetTransform = other.GetComponent<Transform>();
        MovePlayer targetMoveplayer = other.GetComponent<MovePlayer>();
        try
        {
            if (targetHit.isInvincible == false)
            {
                targetHit.SetIsAttackedFromBehind(targetMoveplayer, targetTransform, this.gameObject.transform);
                targetHit.TakeDamage(mediumFireElementalDamage);
            }
        }
        catch (System.NullReferenceException e)
        {
            Debug.Log("The projectile of " + this.gameObject.name + " doesn't touch an enemy character : " + e);
        }
        finally
        {
            Destroy(this.gameObject);
            Instantiate(mediumFireElementalImpactEffect, transform.position, transform.rotation);
        }
    }
}
