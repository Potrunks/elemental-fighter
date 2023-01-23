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
            _elementalBusiness.InflictedDamageAfterCollision(collision, _caster, this, false, destructPowerAfterNoEnemyCollision: false);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.TryGetComponent(out PlayableCharacterController enemy))
            {
                enemy.playableCharacterRigidbody.AddForce((_caster._isLeftFlip ? Vector2.left : Vector2.right) * _powerEntity.powerDamage / 16, ForceMode2D.Impulse);
            }
        }
        #endregion
    }
}
