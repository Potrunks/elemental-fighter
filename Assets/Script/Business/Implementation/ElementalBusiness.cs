using Assets.Script.Business.Interface;
using Assets.Script.Controller;
using Assets.Script.Data;
using Assets.Script.Data.Reference;
using Assets.Script.Entities;
using System.Data;
using System.Linq;
using UnityEngine;
using static Pathfinding.Util.RetainedGizmos;

namespace Assets.Script.Business.Implementation
{
    public class ElementalBusiness : IElementalBusiness
    {
        public void InflictedDamageAfterHitBoxContact(GameObject hitBoxAtk, float hitBoxAtkRadius, bool isPushingAtk, PlayableCharacterController caster, PowerEntity powerEntity)
        {
            Collider2D[] playerColliderTouchedArray = Physics2D.OverlapCircleAll(hitBoxAtk.transform.position, hitBoxAtkRadius, LayerMask.GetMask(new string[] { "Player" }));
            if (playerColliderTouchedArray.Any())
            {
                foreach (Collider2D collider in playerColliderTouchedArray)
                {
                    PlayableCharacterController enemy = collider.GetComponent<PlayableCharacterController>();
                    InflictedElementalAttackDamage(isPushingAtk, caster, enemy, powerEntity);
                }
            }
        }

        public void InstantiateStaticElemental(GameObject elementalToCast, GameObject spawnPoint, PlayableCharacterController caster)
        {
            if (!caster.isLeftFlip)
            {
                spawnPoint.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
            else
            {
                spawnPoint.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            }

            elementalToCast.GetComponent<PowerController>()._casterV2 = caster;

            GameObject.Instantiate(elementalToCast, spawnPoint.transform.position, spawnPoint.transform.rotation);
        }

        public void PrepareCastElemental(PowerEntity powerToCast, GameObject spawnPoint, MovePlayer caster)
        {
            if (powerToCast == null)
            {
                throw new ConstraintException(ElementalConstraintException.NoPowerToCast);
            }

            if (spawnPoint == null)
            {
                throw new ConstraintException(ElementalConstraintException.NoSpawnPoint);
            }

            spawnPoint.transform.rotation = SetAngleOfSpawnPoint(spawnPoint.transform.rotation, powerToCast.powerLevel, powerToCast.powerType);

            PowerController powerController = powerToCast.powerModel.GetComponent<PowerController>();
            powerController._caster = caster;
            powerController._spawnPoint = spawnPoint.transform;
        }

        public void PushMediumElemental(PlayableCharacterController pusher)
        {
            throw new System.NotImplementedException();
        }

        public void RockOutOfGround(PowerController rockPowerControllerInstantiated)
        {
            MovePlayer caster = rockPowerControllerInstantiated._caster;
            PowerEntity rockEntity = rockPowerControllerInstantiated._powerEntity;
            Rigidbody2D rigidbodyOfRock = rockPowerControllerInstantiated._rigidbody;
            Transform spawnPoint = rockPowerControllerInstantiated._spawnPoint;
            int playerIndex = caster.playerIndex;
            AudioSource audioSource = rockEntity.powerSound.audioSource;

            if (rockEntity.powerType != PowerTypeReference.Earth)
            {
                throw new ConstraintException(ElementalConstraintException.ElementalTypeError);
            }

            SetElementalColorByPlayerIndex(rockEntity.powerModel, playerIndex);
            rigidbodyOfRock.AddForce(spawnPoint.right * rockEntity.powerSpeed, ForceMode2D.Impulse);
            if (audioSource != null)
            {
                audioSource.Play();
            }

            Debug.Log("The player " + playerIndex + " take out of the ground a earth elemental");
        }

        public void SetElementalColorByPlayerIndex(GameObject elementalGameObject, int casterPlayerIndex)
        {
            SpriteRenderer elementalSprite = elementalGameObject.GetComponent<SpriteRenderer>();
            switch (casterPlayerIndex)
            {
                case 1:
                    elementalSprite.color = new Color32(133, 136, 253, 255);
                    break;

                case 2:
                    elementalSprite.color = new Color32(141, 253, 134, 255);
                    break;

                case 3:
                    elementalSprite.color = new Color32(245, 253, 133, 255);
                    break;

                default:
                    elementalSprite.color = Color.white;
                    break;
            }
        }

        public void InstantiateElementalUpperOrientation(GameObject elementalToCast, GameObject spawnPoint, PlayableCharacterController caster)
        {
            if (caster.isLeftFlip)
            {
                spawnPoint.transform.rotation = Quaternion.Euler(spawnPoint.transform.rotation.x, spawnPoint.transform.rotation.y, 100);
            }
            else
            {
                spawnPoint.transform.rotation = Quaternion.Euler(spawnPoint.transform.rotation.x, spawnPoint.transform.rotation.y, 80);
            }

            elementalToCast.GetComponent<PowerController>()._casterV2 = caster;

            GameObject.Instantiate(elementalToCast, spawnPoint.transform.position, spawnPoint.transform.rotation);
        }

        /// <summary>
        /// Calculate the angle for the elemental spawn point depending on elemental type and level.
        /// </summary>
        private Quaternion SetAngleOfSpawnPoint(Quaternion spawnPointQuaternion, PowerLevelReference powerLevel, PowerTypeReference powerType)
        {
            Quaternion quaternion = Quaternion.identity;

            if (powerType == PowerTypeReference.Earth
                && powerLevel == PowerLevelReference.Medium)
            {
                if (spawnPointQuaternion.eulerAngles.magnitude == 180)
                {
                    quaternion = Quaternion.Euler(spawnPointQuaternion.x, spawnPointQuaternion.y, 112);
                }
                else
                {
                    quaternion = Quaternion.Euler(spawnPointQuaternion.x, spawnPointQuaternion.y, 67);
                }
            }

            return quaternion;
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

        public void InflictedDamageAfterColliderCollision(Collider2D colliderTouched, PlayableCharacterController caster, PowerController powerController, bool isPushingAtk = false)
        {
            if (!powerController._willBeDestroyed)
            {
                PlayableCharacterController enemy = colliderTouched.GetComponent<PlayableCharacterController>();
                if (enemy != null)
                {
                    if (enemy != caster)
                    {
                        InflictedElementalAttackDamage(isPushingAtk, caster, enemy, powerController._powerEntity);
                        powerController._willBeDestroyed = true;
                    }
                }
                else
                {
                    powerController._willBeDestroyed = true;
                }
            }
        }

        private void InflictedElementalAttackDamage(bool isPushingAtk, PlayableCharacterController caster, PlayableCharacterController enemy, PowerEntity powerEntity)
        {
            if (isPushingAtk)
            {
                if (caster.isLeftFlip)
                {
                    enemy.playableCharacterRigidbody.AddForce(Vector2.left * powerEntity.powerDamage / 16, ForceMode2D.Impulse);
                }
                else
                {
                    enemy.playableCharacterRigidbody.AddForce(Vector2.right * powerEntity.powerDamage / 16, ForceMode2D.Impulse);
                }
            }
            enemy._currentHealth -= powerEntity.powerDamage;
            enemy._lastTouchedBy = caster;
            enemy._isTouchingByAttack = true;
        }
    }
}
