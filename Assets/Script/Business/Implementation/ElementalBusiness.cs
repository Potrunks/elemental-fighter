using Assets.Script.Data;
using Assets.Script.Entities;
using DG.Tweening;
using System.Linq;
using UnityEngine;
using static Pathfinding.Util.RetainedGizmos;

namespace Assets.Script.Business
{
    public class ElementalBusiness : IElementalBusiness
    {
        private ICharacterBusiness _characterBusiness = new CharacterBusiness();

        public void InflictedElementalDamageAfterHitBoxContact(GameObject hitBoxAtk, float hitBoxAtkRadius, bool isPushingAtk, PlayableCharacterController caster, PowerEntity powerEntity)
        {
            Collider2D[] playerColliderTouchedArray = Physics2D.OverlapCircleAll(hitBoxAtk.transform.position, hitBoxAtkRadius, LayerMask.GetMask(new string[] { "Player" }));
            if (playerColliderTouchedArray.Any())
            {
                foreach (Collider2D collider in playerColliderTouchedArray)
                {
                    PlayableCharacterController enemy = collider.GetComponent<PlayableCharacterController>();
                    InflictedElementalDamage(isPushingAtk, caster, enemy, powerEntity);
                }
            }
        }

        public GameObject InstantiateStaticElemental(GameObject elementalToCast, GameObject spawnPoint, PlayableCharacterController caster)
        {
            if (!caster._isLeftFlip)
            {
                spawnPoint.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
            else
            {
                spawnPoint.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            }

            return InstantiateElemental(elementalToCast, spawnPoint, caster);
        }

        public void InstantiateElementalUpperOrientation(GameObject elementalToCast, GameObject spawnPoint, PlayableCharacterController caster)
        {
            if (caster._isLeftFlip)
            {
                spawnPoint.transform.rotation = Quaternion.Euler(spawnPoint.transform.rotation.x, spawnPoint.transform.rotation.y, 100);
            }
            else
            {
                spawnPoint.transform.rotation = Quaternion.Euler(spawnPoint.transform.rotation.x, spawnPoint.transform.rotation.y, 80);
            }

            InstantiateElemental(elementalToCast, spawnPoint, caster);
        }

        public void CheckElementalStateChange(PowerController controller)
        {
            controller.nextState = controller.currentState.CheckingStateModification(controller);
            if (controller.nextState != null)
            {
                controller.currentState.OnExit(controller);
                controller.currentState = controller.nextState;
                controller.currentState.OnEnter(controller);
            }
        }

        public void InflictedDamageAfterCollision(Collider2D colliderTouched, PlayableCharacterController caster, PowerController powerControllerCasted, bool isTriggerAfterCollision, bool destructPowerAfterNoEnemyCollision = true, bool isPushingAtk = false)
        {
            if (!powerControllerCasted._willBeDestroyed)
            {
                if (colliderTouched.TryGetComponent(out PlayableCharacterController enemy))
                {
                    if (enemy != caster)
                    {
                        powerControllerCasted._collider.isTrigger = isTriggerAfterCollision;
                        InflictedElementalDamage(isPushingAtk, caster, enemy, powerControllerCasted._powerEntity);
                        powerControllerCasted._willBeDestroyed = true;
                        powerControllerCasted._isDestroyedAfterDestructiveCollision = true;
                    }
                }
                else
                {
                    powerControllerCasted._willBeDestroyed = destructPowerAfterNoEnemyCollision;
                    powerControllerCasted._isDestroyedAfterDestructiveCollision = destructPowerAfterNoEnemyCollision;
                }
            }
        }

        private void InflictedElementalDamage(bool isPushingAtk, PlayableCharacterController caster, PlayableCharacterController enemy, PowerEntity powerEntity)
        {
            if (!enemy._isInvincible)
            {
                _characterBusiness.RumbleCharacterAfterAtk(enemy, 0.1f, 0.2f, Ease.OutExpo, isPushingAtk, caster._isLeftFlip, powerEntity.powerDamage);
                enemy._currentHealth -= powerEntity.powerDamage;
                enemy._enemy = caster;
                enemy._isTouchingByAttack = true;
            }
        }

        public GameObject InstantiateElemental(GameObject elementalToInstantiate, GameObject spawnPoint, PlayableCharacterController caster)
        {
            elementalToInstantiate.GetComponent<PowerController>()._caster = caster;
            return Object.Instantiate(elementalToInstantiate, spawnPoint.transform.position, spawnPoint.transform.rotation);
        }
    }
}
