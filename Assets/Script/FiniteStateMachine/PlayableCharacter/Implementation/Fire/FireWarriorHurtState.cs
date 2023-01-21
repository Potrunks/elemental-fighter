using Assets.Script.Business.Extension;
using Assets.Script.Data;
using Assets.Script.Data.Reference;
using UnityEngine;

namespace Assets.Script.FiniteStateMachine.PlayableCharacter.Implementation.Fire
{
    public class FireWarriorHurtState : PlayableCharacterStateV2
    {
        private IPlayableCharacterStateV2 nextState;

        public override IPlayableCharacterStateV2 CheckingStateModification(PlayableCharacterController playableCharacterController)
        {
            if (playableCharacterController._isTouchingByAttack)
            {
                return new FireWarriorHurtState();
            }

            if (playableCharacterController.playableCharacterAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            {
                if (playableCharacterController._currentHealth <= 0)
                {
                    return new FireWarriorDieState();
                }
                return new FireWarriorIdleState();
            }

            return nextState;
        }

        public override void OnEnter(PlayableCharacterController controller)
        {
            controller.playableCharacterMoveSpeed = 0;
            controller._bloodEffectForDamage.Play();
            controller.playableCharacterAnimator.Play("Hurt", -1, 0f);
            controller._audioBusiness.PlayRandomSoundEffect(SoundEffectType.HURTING, controller._soundEffectListByType);
            controller._audioBusiness.PlayRandomVoice(VoiceType.HURT, controller._voiceListByType);
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
