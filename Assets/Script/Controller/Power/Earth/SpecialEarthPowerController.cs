using Assets.Script.FiniteStateMachine;
using UnityEngine;

namespace Assets.Script.Controller
{
    public class SpecialEarthPowerController : PowerController
    {
        #region MonoBehaviour Method
        private void Start()
        {
            currentState = new SpecialEarthCastState();
            currentState.OnEnter(this);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            _elementalBusiness.InflictedDamageAfterCollision(collision, _caster, this, false);
        }
        #endregion
    }
}
