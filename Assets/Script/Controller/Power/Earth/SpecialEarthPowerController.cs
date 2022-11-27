using Assets.Script.FiniteStateMachine;
using UnityEngine;

namespace Assets.Script.Controller
{
    public class SpecialEarthPowerController : PowerController
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

            currentState = new SpecialEarthCastState();
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
    }
}
