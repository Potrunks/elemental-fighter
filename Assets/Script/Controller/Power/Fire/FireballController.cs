using Assets.Script.FiniteStateMachine.Power.Implementation.Fire;
using UnityEngine;

namespace Assets.Script.Controller.Power.Fire
{
    public class FireballController : PowerController
    {
        #region Monobehaviour Method
        private void Start()
        {
            currentState = new FireballCastState();
            currentState.OnEnter(this);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            _elementalBusiness.InflictedDamageAfterCollision(collision, _caster, this, true, isPushingAtk: true);
        }
        #endregion
    }
}
