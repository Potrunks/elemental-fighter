﻿using Assets.Script.Business.Extension;
using Assets.Script.Data;
using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
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
                    && !controller.isLeftFlip)
                {
                    controller.transform.Rotate(0f, 180f, 0f);
                    controller.isLeftFlip = true;
                }
                if (controller.playableCharacterRigidbody.velocity.x > GamePlayValueReference.velocityHighThreshold
                    && controller.isLeftFlip)
                {
                    controller.transform.Rotate(0f, 180f, 0f);
                    controller.isLeftFlip = false;
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
            float horizontalMovement = inputMoveValue.x * moveSpeed * Time.deltaTime;
            return Vector3.SmoothDamp(rigidbodyToMove.velocity,
                                      new Vector2(horizontalMovement, rigidbodyToMove.velocity.y),
                                      ref reference,
                                      smoothTime);
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
                RumbleCharacterAfterAtk(enemy, 0.1f, 0.2f, Ease.OutExpo, isPushingAtk, caster.isLeftFlip, caster.playableCharacter.AttackForce);
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
                        elemental._rigidbody.AddForce(elemental.transform.right * (elemental._powerEntity.powerSpeed * 2), ForceMode2D.Impulse);
                        elemental._collider.isTrigger = true;
                    }
                }
            }
        }

        public void RespawnPlayer(PlayableCharacterController characterToRespawn)
        {
            Debug.Log("The player " + (characterToRespawn._playerIndex + 1) + " respawn !!!");
            characterToRespawn.transform.position = characterToRespawn._spawnPlayerPoint.transform.position;
            characterToRespawn._currentHealth = characterToRespawn.playableCharacter.MaxHealth;
            characterToRespawn.TriggerInvincibility(3f);
        }

        public bool ResetCharacterAfterDeath(PlayableCharacterController characterDied)
        {
            Debug.Log("The player " + (characterDied._playerIndex + 1) + " just died... Reset character in progress...");
            bool hasEnemyWon = UpdateCharacterScore(characterDied._enemy._scorePlayer);
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

        public float? DoBleedingEffect(int currentHealth, int maxHealth, float? nextBleedingTime, ParticleSystem bleedingEffectParticle)
        {
            if (nextBleedingTime == null && currentHealth <= maxHealth.PercentageOf(GamePlayValueReference.START_PERCENTAGE_BLEEDING))
            {
                return PrepareNextBleedingTime(currentHealth, maxHealth, nextBleedingTime);
            }

            if (nextBleedingTime != null && Time.time > nextBleedingTime)
            {
                return PlayBleedingEffect(bleedingEffectParticle);
            }

            return nextBleedingTime;
        }

        private float? PrepareNextBleedingTime(int currentHealth, int maxHealth, float? nextBleedingTime)
        {
            float percentageCurrentHealth = currentHealth.ToPercentage(maxHealth);

            switch (percentageCurrentHealth)
            {
                case >= 60f:
                    nextBleedingTime = Time.time + 4f;
                    break;
                case >= 40f:
                    nextBleedingTime = Time.time + 3f;
                    break;
                case >= 20f:
                    nextBleedingTime = Time.time + 2f;
                    break;
                case > 0f:
                    nextBleedingTime = Time.time + 1f;
                    break;
            }

            return nextBleedingTime;
        }

        private float? PlayBleedingEffect(ParticleSystem bleedingEffectParticle)
        {
            bleedingEffectParticle.Play();

            return null;
        }
    }
}
