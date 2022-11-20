using Assets.Script.Business.Implementation;
using Assets.Script.Business.Interface;
using Assets.Script.Entities;
using Assets.Script.FiniteStateMachine;
using UnityEngine;

public class PowerController : MonoBehaviour
{
    public PowerEntity powerEntity;
    public float selfDestructTimer;
    public MovePlayer caster;
    public Transform elementalSpawnPointTransform;
    public Rigidbody2D rb;

    public IElementalBusiness elementalBusiness;
    public IPowerState powerState;

    private void Awake()
    {
        elementalBusiness = new ElementalBusiness();
        rb = this.gameObject.GetComponent<Rigidbody2D>();

        if (selfDestructTimer != 0)
        {
            Destroy(this.gameObject, selfDestructTimer);
        }
    }
}
