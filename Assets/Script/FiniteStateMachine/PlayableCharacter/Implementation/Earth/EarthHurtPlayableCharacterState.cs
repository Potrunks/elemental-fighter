using Assets.Script.Business.Extension;
using Assets.Script.Data;
using Assets.Script.Data.Reference;
using Assets.Script.FiniteStateMachine.PlayableCharacter.Implementation.Fire;
using UnityEngine;

namespace Assets.Script.FiniteStateMachine
{
    public class EarthHurtPlayableCharacterState : PlayableCharacterStateV2
    {
        IPlayableCharacterStateV2 nextState;

        public override IPlayableCharacterStateV2 CheckingStateModification(PlayableCharacterController playableCharacterController)
        {
            if (playableCharacterController._isTouchingByAttack)
            {
                return new EarthHurtPlayableCharacterState();
            }

            if (playableCharacterController.playableCharacterAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
            {
                if (playableCharacterController._currentHealth <= 0)
                {
                    return new EarthDiePlayableCharacterState();
                }
                return new EarthIdlePlayableCharacterState();
            }

            return nextState;
        }

        public override void OnEnter(PlayableCharacterController controller)
        {
            controller.playableCharacterMoveSpeed = 0;
            controller._bloodEffectForDamage.Play();
            controller.playableCharacterAnimator.Play("Hurt", -1, 0f);
            controller._audioBusiness.PlayRandomSoundEffect(SoundEffectType.HURTING, controller._soundEffectListByType);
            if (controller._currentHealth <= controller.playableCharacter.MaxHealth.PercentageOf(GamePlayValueReference.START_PERCENTAGE_BLEEDING)
                && !controller._isBleeding)
            {
                controller._isBleeding = true;
                controller.StartCoroutine(controller.DoBleedingCoroutine());
            }
        }

        public override void OnExit(PlayableCharacterController playableCharacterController)
        {
            playableCharacterController.playableCharacterMoveSpeed = playableCharacterController.playableCharacter.MoveSpeed;
            playableCharacterController._isTouchingByAttack = false;
        }

        public override void PerformingInput(PlayableCharacterActionReference action)
        {
            switch (action)
            {
                default:
                    Debug.LogWarning(GamePlayConstraintException.ActionNotPermitted + action);
                    nextState = null;
                    break;
            }
        }
    }
}
