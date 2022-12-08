﻿using Assets.Script.Data;
using Assets.Script.Entities;
using DG.Tweening;
using System.Linq;
using UnityEngine;

namespace Assets.Script.Business
{
    public class ElementalBusiness : IElementalBusiness
    {
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
            if (!caster.isLeftFlip)
            {
                spawnPoint.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
            else
            {
                spawnPoint.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            }

            elementalToCast.GetComponent<PowerController>()._caster = caster;

            return GameObject.Instantiate(elementalToCast, spawnPoint.transform.position, spawnPoint.transform.rotation);
        }

        public void PushMediumElemental(PlayableCharacterController pusher)
        {
            throw new System.NotImplementedException();
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

            elementalToCast.GetComponent<PowerController>()._caster = caster;

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

        public void InflictedDamageAfterCollision(Collider2D colliderTouched, PlayableCharacterController caster, PowerController powerControllerCasted, bool isTriggerAfterCollision, bool isPushingAtk = false)
        {
            if (!powerControllerCasted._willBeDestroyed)
            {
                powerControllerCasted._collider.isTrigger = isTriggerAfterCollision;
                PlayableCharacterController enemy = colliderTouched.GetComponent<PlayableCharacterController>();
                if (enemy != null)
                {
                    if (enemy != caster)
                    {
                        InflictedElementalDamage(isPushingAtk, caster, enemy, powerControllerCasted._powerEntity);
                        powerControllerCasted._willBeDestroyed = true;
                    }
                }
                else
                {
                    powerControllerCasted._willBeDestroyed = true;
                }
            }
        }

        private void InflictedElementalDamage(bool isPushingAtk, PlayableCharacterController caster, PlayableCharacterController enemy, PowerEntity powerEntity)
        {
            if (!enemy._isInvincible)
            {
                caster.characterBusiness.RumbleCharacterAfterAtk(enemy, 0.1f, 0.2f, Ease.OutExpo, isPushingAtk, caster.isLeftFlip, powerEntity.powerDamage);
                enemy._currentHealth -= powerEntity.powerDamage;
                enemy._enemy = caster;
                enemy._isTouchingByAttack = true;
            }
        }
    }
}
