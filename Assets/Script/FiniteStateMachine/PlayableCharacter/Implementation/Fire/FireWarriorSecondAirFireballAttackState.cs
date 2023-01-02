using Assets.Script.Data;
using Assets.Script.Data.Reference;
using UnityEngine;

namespace Assets.Script.FiniteStateMachine.PlayableCharacter.Implementation.Fire
{
    public class FireWarriorSecondAirFireballAttackState : PlayableCharacterStateV2
    {
        private IPlayableCharacterStateV2 nextState;

        public override IPlayableCharacterStateV2 CheckingStateModification(PlayableCharacterController playableCharacterController)
        {
            if (playableCharacterController.isGrounding)
            {
                return new FireWarriorIdleState();
            }

            if (playableCharacterController.playableCharacterAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            {
                return new FireWarriorSecondAirFireballAttackTransitionState();
            }

            return nextState;
        }

        public override void OnEnter(PlayableCharacterController playableCharacterController)
        {
            playableCharacterController.playableCharacterAnimator.Play("AirMediumAttack2");
            playableCharacterController._audioBusiness.PlayRandomSoundEffect(SoundEffectType.ELEMENTAL_CASTING, playableCharacterController._soundEffectListByType);
        }

        public override void OnExit(PlayableCharacterController playableCharacterController)
        {
            
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
