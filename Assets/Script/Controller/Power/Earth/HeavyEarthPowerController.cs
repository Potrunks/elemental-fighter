using Assets.Script.FiniteStateMachine;
using System.Linq;
using UnityEngine;
using static Pathfinding.Util.RetainedGizmos;

namespace Assets.Script.Controller
{
    public class HeavyEarthPowerController : PowerController
    {
        [Header("HitBox")]
        [SerializeField]
        GameObject _hitBoxAtk;
        [SerializeField]
        float _hitBoxAtkRadius;

        #region MonoBehaviour Method
        private void Start()
        {
            _hitBoxAtk = transform.Find("HitBoxAtk").gameObject;

            currentState = new HeavyEarthCastState();
            currentState.OnEnter(this);
        }
        #endregion

        #region Gizmos
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_hitBoxAtk.transform.position, _hitBoxAtkRadius);
        }
        #endregion

        #region Action
        public void OnThrowHeavyEarthElemental()
        {
            _elementalBusiness.InflictedElementalDamageAfterHitBoxContact(_hitBoxAtk, _hitBoxAtkRadius, true, _casterV2, _powerEntity);
        }
        #endregion
    }
}
