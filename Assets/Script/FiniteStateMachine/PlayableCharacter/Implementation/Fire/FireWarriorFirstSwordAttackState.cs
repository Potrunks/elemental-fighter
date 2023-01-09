using Assets.Script.Data;
using Assets.Script.Data.Reference;
using UnityEngine;

namespace Assets.Script.FiniteStateMachine.PlayableCharacter.Implementation.Fire
{
    internal class FireWarriorFirstSwordAttackState : PlayableCharacterStateV2
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
                return new FireWarriorFirstSwordAttackTransitionState();
            }

            return nextState;
        }

        public override void OnEnter(PlayableCharacterController playableCharacterController)
        {
            playableCharacterController.playableCharacterMoveSpeed = 0;
            playableCharacterController.playableCharacterAnimator.Play("LightATK1");
            playableCharacterController._audioBusiness.PlayRandomSoundEffect(SoundEffectType.SWORD_ATTACKING, playableCharacterController._soundEffectListByType);
            playableCharacterController._audioBusiness.PlayRandomVoice(VoiceType.LIGHT_ATTACK, playableCharacterController._voiceListByType);
        }

        public override void OnExit(PlayableCharacterController playableCharacterController)
        {
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
