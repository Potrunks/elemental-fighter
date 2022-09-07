using Assets.Script.Business.Implementation;
using Assets.Script.Business.Interface;
using UnityEngine;

public class MediumFireElementalCommand : MonoBehaviour
{
    public float speed;
    public int mediumFireElementalDamage;
    public Rigidbody2D rb;
    public GameObject mediumFireElementalImpactEffect;
    public MovePlayer caster;
    public Transform elementalSpawnPointTransform;

    private IElementalBusiness elementalBusiness = new ElementalBusiness();

    void Start()
    {
        /*
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
        */
        elementalBusiness.SetElementalColorByPlayerIndex(this.gameObject, caster.playerIndex);
        rb.AddForce(elementalSpawnPointTransform.right * speed, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        MovePlayer targetMoveplayer = other.GetComponent<MovePlayer>();
        if (caster != targetMoveplayer)
        {
            DamageCommand targetHit = other.GetComponent<DamageCommand>();
            Transform targetTransform = other.GetComponent<Transform>();
            try
            {
                if (targetHit.isInvincible == false)
                {
                    caster.SetPlayerAsEnemy(targetMoveplayer);
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
}
