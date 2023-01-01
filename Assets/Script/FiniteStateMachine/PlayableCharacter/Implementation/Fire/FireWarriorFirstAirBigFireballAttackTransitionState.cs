using Assets.Script.Data;
using UnityEngine;

namespace Assets.Script.FiniteStateMachine.PlayableCharacter.Implementation.Fire
{
    public class FireWarriorFirstAirBigFireballAttackTransitionState : PlayableCharacterStateV2
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
                if (playableCharacterController.playableCharacterRigidbody.velocity.y >= GamePlayValueReference.velocityHighThreshold)
                {
                    return new FireWarriorJumpState();
                }

                if (playableCharacterController.playableCharacterRigidbody.velocity.y >= GamePlayValueReference.velocityLowThreshold)
                {
                    return new FireWarriorFallState();
                }
            }

            return nextState;
        }

        public override void OnEnter(PlayableCharacterController playableCharacterController)
        {
            playableCharacterController.playableCharacterAnimator.Play("AirHeavyAttack1Transition");
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
