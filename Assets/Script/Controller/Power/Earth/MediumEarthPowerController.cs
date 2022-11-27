using Assets.Script.FiniteStateMachine;
using UnityEngine;

namespace Assets.Script.Controller
{
    public class MediumEarthPowerController : PowerController
    {
        #region MonoBehaviour Method
        private void Start()
        {
            currentState = new MediumEarthCastState();
            currentState.OnEnter(this);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            _elementalBusiness.InflictedElementalDamageAfterColliderCollision(collision, _casterV2, this, isPushingAtk: true);
        }
        #endregion
    }
}
