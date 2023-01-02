using Assets.Script.Data;
using UnityEngine;

namespace Assets.Script.FiniteStateMachine.PlayableCharacter.Implementation.Fire
{
    public class FireWarriorFirstFireballAttackTransitionState : PlayableCharacterStateV2
    {
        private IPlayableCharacterStateV2 nextState;

        public override IPlayableCharacterStateV2 CheckingStateModification(PlayableCharacterController playableCharacterController)
        {
            if (playableCharacterController.playableCharacterAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            {
                return new FireWarriorIdleState();
            }

            return nextState;
        }

        public override void OnEnter(PlayableCharacterController playableCharacterController)
        {
            playableCharacterController.playableCharacterAnimator.Play("MediumATK1Transition");
        }

        public override void OnExit(PlayableCharacterController playableCharacterController)
        {
            
        }

        public override void PerformingInput(PlayableCharacterActionReference action)
        {
            switch (action)
            {
                case PlayableCharacterActionReference.MediumAtk:
                    nextState = new FireWarriorSecondFireballAttackState();
                    break;
                case PlayableCharacterActionReference.HeavyAtk:
                    nextState = new FireWarriorFirstBigFireballAttackState();
                    break;
                default:
                    Debug.LogWarning(GamePlayConstraintException.ActionNotPermitted + action);
                    nextState = null;
                    break;
            }
        }
    }
}
