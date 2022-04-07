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
        try
        {
            other.GetComponent<DamageCommand>().TakeDamage(mediumFireElementalDamage);
        }
        finally
        {
            Destroy(this.gameObject);
            Instantiate(mediumFireElementalImpactEffect, transform.position, transform.rotation);
        }
    }
}
