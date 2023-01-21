﻿using Assets.Script.Business.Extension;
using Assets.Script.Business.Job;
using Assets.Script.Business.Tools;
using Assets.Script.Data;
using Assets.Script.Data.ConstraintException;
using Assets.Script.Data.Reference;
using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Script.Business
{
    public class CharacterBusiness : ICharacterBusiness
    {
        private static readonly System.Random random = new System.Random();

        private IVFXBusiness vFXBusiness = new VFXBusiness();

        public void ConfirmCharacter(GameObject playerslot, GameObject characterUnderCursor)
        {
            vFXBusiness.ClearTweenEffectOfImageComponent(characterUnderCursor, "Border");
            playerslot.transform.DOPunchPosition(Vector3.down * 3, .3f, 10, 1);
        }

        public void CheckFlipCharacterModel(PlayableCharacterController controller)
        {
            if (controller.isDeviceUsed)
            {
                if (controller.playableCharacterRigidbody.velocity.x < GamePlayValueReference.velocityLowThreshold
                    && !controller._isLeftFlip)
                {
                    controller.transform.Rotate(0f, 180f, 0f);
                    controller._isLeftFlip = true;
                }
                if (controller.playableCharacterRigidbody.velocity.x > GamePlayValueReference.velocityHighThreshold
                    && controller._isLeftFlip)
                {
                    controller.transform.Rotate(0f, 180f, 0f);
                    controller._isLeftFlip = false;
                }
            }
        }

        public GameObject GetRandomCharacter()
        {
            List<CharacterPreview> characterPreviewListWoRandom = SelectCharacterManager.instance.characterPreviewList.Where(cp => cp.characterPrefab != null).ToList();
            int randomInt = random.Next(characterPreviewListWoRandom.Count);
            return characterPreviewListWoRandom.ElementAt(randomInt).characterPrefab;
        }

        public Vector2 MoveCharacter(Vector2 inputMoveValue, float moveSpeed, Rigidbody2D rigidbodyToMove, float smoothTime)
        {
            Vector3 reference = Vector3.zero;
            NativeArray<float> result = new NativeArray<float>(1, Allocator.TempJob);
            NativeArray<float> floatToMultiplicate = new NativeArray<float>(3, Allocator.TempJob);
            floatToMultiplicate[0] = inputMoveValue.x;
            floatToMultiplicate[1] = moveSpeed;
            floatToMultiplicate[2] = Time.deltaTime;
            MultiplicatorJob job = new MultiplicatorJob
            {
                floatListToMultiplicate = floatToMultiplicate,
                resultMultiplicator = result
            };
            JobHandle jobHandle = job.Schedule();
            jobHandle.Complete();
            Vector2 vector2 = Vector3.SmoothDamp(rigidbodyToMove.velocity,
                                      new Vector2(result[0], rigidbodyToMove.velocity.y),
                                      ref reference,
                                      smoothTime);
            result.Dispose();
            floatToMultiplicate.Dispose();
            return vector2;
        }

        public void ShowCharacterInPlayerSlot(GameObject playerSlot, GameObject characterUnderPlayerCursor)
        {
            Transform playerSlotImageTransform = playerSlot.transform.Find("Image");
            Image playerSlotImage = playerSlotImageTransform.GetComponent<Image>();
            playerSlotImage.sprite = null;
            playerSlotImage.color = Color.clear;
            TextMeshProUGUI playerSlotCharacterName = playerSlot.transform.Find("CharacterNameSelected").GetComponent<TextMeshProUGUI>();
            playerSlotCharacterName.text = "No player selected";

            if (characterUnderPlayerCursor != null)
            {
                Image characterUnderPlayerCursorImage = characterUnderPlayerCursor.transform.Find("Image").GetComponent<Image>();
                Sequence s = DOTween.Sequence();
                s.Append(playerSlotImageTransform.DOLocalMoveX(-100, 0.05f).SetEase(Ease.OutCubic));
                s.AppendCallback(() => playerSlotImage.sprite = characterUnderPlayerCursorImage.sprite);
                s.AppendCallback(() => playerSlotImage.color = Color.white);
                s.Append(playerSlotImageTransform.DOLocalMoveX(100, 0));
                s.Append(playerSlotImageTransform.DOLocalMoveX(0, 0.05f).SetEase(Ease.OutCubic));
                TextMeshProUGUI characterUnderPlayerCursorText = characterUnderPlayerCursor.transform.Find("Name").GetComponent<TextMeshProUGUI>();
                playerSlotCharacterName.text = characterUnderPlayerCursorText.text;
            }
        }

        public void CheckCharacterStateChange(PlayableCharacterController controller)
        {
            controller.nextState = controller.currentState.CheckingStateModification(controller);
            if (controller.nextState != null)
            {
                controller.currentState.OnExit(controller);
                controller.currentState = controller.nextState;
                controller.currentState.OnEnter(controller);
            }
        }

        public void InflictedMeleeDamageAfterHitBoxContact(GameObject hitBox, float hitBoxRadius, PlayableCharacterController caster, bool isPushingAtk = false)
        {
            Collider2D[] enemyColliderArray = Physics2D.OverlapCircleAll(hitBox.transform.position, hitBoxRadius, LayerMask.GetMask(new string[] {"Player"}));

            if (enemyColliderArray.Any())
            {
                foreach (Collider2D enemyCollider in enemyColliderArray)
                {
                    InflictedMeleeDamage(enemyCollider.GetComponent<PlayableCharacterController>(), caster, isPushingAtk);
                }
            }
        }

        public void InflictedMeleeDamage(PlayableCharacterController enemy, PlayableCharacterController caster, bool isPushingAtk)
        {
            if (!enemy._isInvincible)
            {
                RumbleCharacterAfterAtk(enemy, 0.1f, 0.2f, Ease.OutExpo, isPushingAtk, caster._isLeftFlip, caster.playableCharacter.AttackForce);
                enemy._currentHealth -= caster.playableCharacter.AttackForce;
                enemy._enemy = caster;
                enemy._isTouchingByAttack = true;
            }
        }

        public void PushElementalProjectile(PlayableCharacterController pusher, IEnumerable<PowerLevelReference> powerLevelToPushList, float selfDestructTimer, IEnumerable<RigidbodyConstraints2D> rigidbodyConstraints2DList = null)
        {
            Collider2D[] elementalColliderListTouched = Physics2D.OverlapCircleAll(pusher._hitBoxAtk.transform.position, pusher._hitBoxAtkRadius, LayerMask.GetMask(new string[] { "ElementalProjectile", "ElementalProjectileMountable" }));

            if (elementalColliderListTouched.Any())
            {
                foreach (Collider2D elementalCollider in elementalColliderListTouched)
                {
                    PowerController elemental = elementalCollider.GetComponent<PowerController>();
                    if (elemental != null && elemental._caster.Equals(pusher) && powerLevelToPushList.Contains(elemental._powerEntity.powerLevel))
                    {
                        elemental.TriggerSelfDestruct(selfDestructTimer);
                        elemental._rigidbody.constraints = pusher._physicsBusiness.ApplyRigidbodyConstraint2D(rigidbodyConstraints2DList);
                        elemental.transform.rotation = elemental._rigidbody.constraints.HasFlag(RigidbodyConstraints2D.FreezeRotation) ? elemental.transform.rotation : pusher.gameObjectElementalSpawnPoint.transform.rotation;
                        elemental._rigidbody.AddForce(elemental.transform.right * (elemental._powerEntity.powerSpeed * 2 * AxisModificatorTools.DependCharacterAndElementalOrientation(pusher._isLeftFlip, elemental.transform.rotation.y)), ForceMode2D.Impulse);
                        elemental._collider.isTrigger = true;
                    }
                }
            }
        }

        public void RespawnPlayer(PlayableCharacterController characterToRespawn)
        {
            Debug.Log("The player " + (characterToRespawn._playerIndex + 1) + " respawn !!!");
            characterToRespawn.transform.position = characterToRespawn._spawnPlayerPoint == null ? characterToRespawn.transform.position : characterToRespawn._spawnPlayerPoint.transform.position;
            characterToRespawn._currentHealth = characterToRespawn.playableCharacter.MaxHealth;
            characterToRespawn.StartCoroutine(characterToRespawn.DoInvincibleCoroutine(GamePlayValueReference.INVINCIBLE_DURATION));
        }

        public bool ResetCharacterAfterDeath(PlayableCharacterController characterDied)
        {
            Debug.Log("The player " + (characterDied._playerIndex + 1) + " just died... Reset character in progress...");
            bool hasEnemyWon = characterDied._enemy._scorePlayer == null ? false : UpdateCharacterScore(characterDied._enemy._scorePlayer);
            if (hasEnemyWon)
            {
                GameManager.instance.DisplayEndgameResults();
            }
            else
            {
                RespawnPlayer(characterDied);
            }
            return hasEnemyWon;
        }

        public bool UpdateCharacterScore(ScorePlayer scorePlayerToUpdate, int pointWon = 1)
        {
            Debug.Log("The player " + (scorePlayerToUpdate.playerIndex + 1) + " win " + pointWon + " point(s)");
            scorePlayerToUpdate.victoryPoint += pointWon;
            scorePlayerToUpdate.DisplayScore();
            if (scorePlayerToUpdate.victoryPoint == GameManager.instance.victoryPointCondition)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void RumbleCharacterAfterAtk(PlayableCharacterController characterToRumble, float rumbleDuration, float rumbleStrength, Ease easeStyle, bool isPushingAtk, bool casterIsFlipLeft, int atkPower)
        {
            characterToRumble.transform.DOShakePosition(rumbleDuration, strength: rumbleStrength)
                               .SetEase(easeStyle)
                               .OnComplete(() =>
                               {
                                   if (isPushingAtk)
                                   {
                                       characterToRumble.playableCharacterRigidbody.AddForce((casterIsFlipLeft ? Vector2.left : Vector2.right) * atkPower / 8, ForceMode2D.Impulse);
                                   }
                               });
        }

        public float DoBleedingEffect(int currentHealth, int maxHealth, ParticleSystem bleedingEffectParticle)
        {
            float percentageCurrentHealth = currentHealth.ToPercentage(maxHealth);
            float nextBleedingTime = 0;

            switch (percentageCurrentHealth)
            {
                case >= 60:
                    nextBleedingTime = 4;
                    break;
                case >= 40:
                    nextBleedingTime = 3;
                    break;
                case >= 20:
                    nextBleedingTime = 2;
                    break;
                case > 0:
                    nextBleedingTime = 1;
                    break;
            }

            bleedingEffectParticle.Play();

            return nextBleedingTime;
        }

        public int ReturnBlockedDamage(int healthAfterDamage, int healthBeforeDamage, int damageReductionFactor)
        {
            if (healthAfterDamage <= 0 || healthBeforeDamage <= 0 || damageReductionFactor <= 0)
            {
                throw new ImpossibleValueConstraintException(ImpossibleValueConstraintExceptionMessageReference.NEGATIVE_VALUE_NOT_PERMITTED, new List<int> { healthAfterDamage, healthBeforeDamage, damageReductionFactor});
            }

            if (healthBeforeDamage <= healthAfterDamage)
            {
                throw new ImpossibleValueConstraintException(ImpossibleValueConstraintExceptionMessageReference.NOT_LOGIC_BETWEEN_VALUES, healthAfterDamage, healthBeforeDamage);
            }

            int damage = healthBeforeDamage - healthAfterDamage;
            return damage / damageReductionFactor * (damageReductionFactor - 1);
        }
    }
}
