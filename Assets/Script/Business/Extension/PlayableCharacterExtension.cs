﻿using UnityEngine;

namespace Assets.Script.Business
{
    public static class PlayableCharacterExtension
    {
        /// <summary>
        /// Active the invincibility of the character with an invincibility duration (in second)
        /// and the color of the character become more transparent.
        /// </summary>
        public static void TriggerInvincibility(this PlayableCharacterController characterToActivateInvincibility, float invincibilityDuration)
        {
            characterToActivateInvincibility._isInvincible = true;
            characterToActivateInvincibility._invincibleLimitTimer = Time.time + invincibilityDuration;
            characterToActivateInvincibility._spriteRenderer.material.color = new Color(
                                                                                            characterToActivateInvincibility._spriteRenderer.material.color.r,
                                                                                            characterToActivateInvincibility._spriteRenderer.material.color.g,
                                                                                            characterToActivateInvincibility._spriteRenderer.material.color.b,
                                                                                            0.25f
                                                                                        );
        }

        /// <summary>
        /// Check if the player stay invincible or not.
        /// </summary>
        public static bool CheckInvincibleEndTime(this PlayableCharacterController character)
        {
            if (character._isInvincible)
            {
                if (Time.time > character._invincibleLimitTimer)
                {
                    character._spriteRenderer.material.color = new Color(
                                                                        character._spriteRenderer.material.color.r,
                                                                        character._spriteRenderer.material.color.g,
                                                                        character._spriteRenderer.material.color.b,
                                                                        1f
                                                                    );
                    return false;
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// Update score of the player who just died by a fall or zero health.
        /// </summary>
        public static void UpdateScoreAfterFell(this PlayableCharacterController characterToUpdateScore)
        {
            if (characterToUpdateScore._enemy == null)
            {
                characterToUpdateScore._scorePlayer.Suicide();
            }
            else
            {
                characterToUpdateScore._enemy._scorePlayer.UpdateScore();
            }
        }

        /// <summary>
        /// Return true if the character is flip left.
        /// </summary>
        public static bool IsFlipLeft(this PlayableCharacterController character)
        {
            return character.transform.rotation.y == - 1 ? true : false;
        }
    }
}
