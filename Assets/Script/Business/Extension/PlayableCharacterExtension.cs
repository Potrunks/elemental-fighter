using UnityEngine;

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
    }
}
