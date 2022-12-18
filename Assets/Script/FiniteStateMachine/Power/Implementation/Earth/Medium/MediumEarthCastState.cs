using Assets.Script.Data.Reference;
using UnityEngine;

namespace Assets.Script.FiniteStateMachine
{
    public class MediumEarthCastState : PowerState
    {
        public override IPowerState CheckingStateModification(PowerController powerController)
        {
            if (powerController._willBeDestroyed)
            {
                return nextState = new MediumEarthDestroyState();
            }

            return nextState;
        }

        public override void OnEnter(PowerController powerController)
        {
            powerController._rigidbody.AddForce(powerController.transform.right * (powerController._powerEntity.powerSpeed / 2), ForceMode2D.Impulse);
            powerController._audioBusiness.PlayRandomSoundEffect(SoundEffectType.ELEMENTAL_CASTING, powerController._soundEffectByType);
        }

        public override void OnExit(PowerController powerController)
        {
            
        }
    }
}
