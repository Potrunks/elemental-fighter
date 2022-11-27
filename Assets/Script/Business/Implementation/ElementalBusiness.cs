using Assets.Script.Business.Interface;
using Assets.Script.Controller;
using Assets.Script.Data;
using Assets.Script.Data.Reference;
using Assets.Script.Entities;
using System.Data;
using System.Linq;
using UnityEngine;

namespace Assets.Script.Business.Implementation
{
    public class ElementalBusiness : IElementalBusiness
    {
        public void CheckObjectTouchedByAttack(GameObject hitBoxAtk, float hitBoxAtkRadius)
        {
            Collider2D[] playerTouchedColliderArray = Physics2D.OverlapCircleAll(hitBoxAtk.transform.position, hitBoxAtkRadius, LayerMask.GetMask(new string[] { "Player" }));

            if (playerTouchedColliderArray.Any())
            {
                foreach (Collider2D playerTouchedCollider in playerTouchedColliderArray)
                {
                    // Recup le controller de l'ennemi touché
                    // On lui fait prendre des degat
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

        public void InflictedDamageAfterCollision(Collider2D colliderTouched, PlayableCharacterController caster, PowerController powerController, bool isPushingAtk = false)
        {
            if (!powerController._willBeDestroyed)
            {
                PlayableCharacterController playerTouched = colliderTouched.GetComponent<PlayableCharacterController>();
                if (playerTouched != null)
                {
                    if (playerTouched != caster)
                    {
                        powerController._characterBusiness.InflictedDamage(playerTouched, caster, isPushingAtk);
                        powerController._willBeDestroyed = true;
                    }
                }
                else
                {
                    powerController._willBeDestroyed = true;
                }
            }
        }
    }
}
