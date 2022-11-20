using Assets.Script.Business.Interface;
using Assets.Script.Controller;
using Assets.Script.Data;
using Assets.Script.Data.Reference;
using Assets.Script.Entities;
using System.Data;
using UnityEngine;

namespace Assets.Script.Business.Implementation
{
    public class ElementalBusiness : IElementalBusiness
    {
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
            powerController.caster = caster;
            powerController.elementalSpawnPointTransform = spawnPoint.transform;
        }

        public void RockOutOfGround(PowerController rockPowerControllerInstantiated)
        {
            MovePlayer caster = rockPowerControllerInstantiated.caster;
            PowerEntity rockEntity = rockPowerControllerInstantiated.powerEntity;
            Rigidbody2D rigidbodyOfRock = rockPowerControllerInstantiated.rb;
            Transform spawnPoint = rockPowerControllerInstantiated.elementalSpawnPointTransform;
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
    }
}
