using Assets.Script.Business.Implementation;
using Assets.Script.Business.Interface;
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

    private IElementalBusiness elementalBusiness = new ElementalBusiness();

    void Start()
    {
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
}
