using Assets.Script.Data.Reference;
using UnityEngine;

namespace Assets.Script.FiniteStateMachine.Power.Implementation.Fire
{
    public class FireballDestroyState : PowerState
    {
        private AudioSource destroySoundPlayed;

        public override IPowerState CheckingStateModification(PowerController powerController)
        {
            if (!powerController._destroyEffect.isPlaying)
            {
                if (destroySoundPlayed != null && !destroySoundPlayed.isPlaying)
                {
                    Object.Destroy(powerController.gameObject);
                    return null;
                }
                else if (destroySoundPlayed == null)
                {
                    Object.Destroy(powerController.gameObject);
                    return null;
                }
            }

            return nextState;
        }

        public override void OnEnter(PowerController powerController)
        {
            powerController._destroyEffect.Play();
            powerController._spriteRenderer.enabled = false;
            if (powerController._isDestroyedAfterDestructiveCollision)
            {
                destroySoundPlayed = powerController._audioBusiness.PlayRandomSoundEffect(SoundEffectType.ELEMENTAL_DESTROYING, powerController._soundEffectByType);
            }
        }

        public override void OnExit(PowerController powerController)
        {
            
        }
    }
}
