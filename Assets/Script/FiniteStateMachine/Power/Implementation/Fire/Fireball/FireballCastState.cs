using Assets.Script.Data.Reference;
using System;
using UnityEngine;

namespace Assets.Script.FiniteStateMachine.Power.Implementation.Fire
{
    public class FireballCastState : PowerState
    {
        public override IPowerState CheckingStateModification(PowerController powerController)
        {
            if (powerController._willBeDestroyed)
            {
                return nextState = new FireballDestroyState();
            }

            return nextState;
        }

        public override void OnEnter(PowerController powerController)
        {
            powerController._animator.Play("Throwing");
            powerController._rigidbody.AddForce(powerController.transform.right * powerController._powerEntity.powerSpeed, ForceMode2D.Impulse);
            powerController._audioBusiness.PlayRandomSoundEffect(SoundEffectType.ELEMENTAL_CASTING, powerController._soundEffectByType);
        }

        public override void OnExit(PowerController powerController)
        {
            
        }
    }
}
