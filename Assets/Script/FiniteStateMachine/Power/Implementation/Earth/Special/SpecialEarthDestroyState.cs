using Assets.Script.Data.Reference;
using UnityEngine;

namespace Assets.Script.FiniteStateMachine
{
    public class SpecialEarthDestroyState : PowerState
    {
        public override IPowerState CheckingStateModification(PowerController powerController)
        {
            if (powerController._animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
            {
                GameObject.Destroy(powerController.gameObject);
                return nextState = null;
            }

            return nextState;
        }

        public override void OnEnter(PowerController powerController)
        {
            powerController._animator.Play("Destroying");
            if (powerController._isDestroyedAfterDestructiveCollision)
            {
                powerController._audioBusiness.PlayRandomSoundEffect(SoundEffectType.ELEMENTAL_DESTROYING, powerController._soundEffectByType);
            }
        }

        public override void OnExit(PowerController powerController)
        {
            
        }
    }
}
