using Assets.Script.Data;
using UnityEngine;

namespace Assets.Script.FiniteStateMachine.PlayableCharacter.Implementation.Fire
{
    public class FireWarriorIdleState : PlayableCharacterStateV2
    {
        private IPlayableCharacterStateV2 nextState;

        public override IPlayableCharacterStateV2 CheckingStateModification(PlayableCharacterController playableCharacterController)
        {
            if (nextState != null)
            {
                return nextState;
            }

            if (playableCharacterController.isDeviceUsed
                && (playableCharacterController.playableCharacterRigidbody.velocity.x > GamePlayValueReference.velocityHighThreshold
                    || playableCharacterController.playableCharacterRigidbody.velocity.x < GamePlayValueReference.velocityLowThreshold))
            {
                return new FireWarriorMoveState();
            }

            return null;
        }

        public override void OnEnter(PlayableCharacterController playableCharacterController)
        {
            playableCharacterController.playableCharacterMoveSpeed = playableCharacterController.playableCharacter.MoveSpeed;
            playableCharacterController.playableCharacterAnimator.Play("Idle", -1, 0);
        }

        public override void OnExit(PlayableCharacterController playableCharacterController)
        {
            
        }

        public override void PerformingInput(PlayableCharacterActionReference action)
        {
            switch (action)
            {
                case PlayableCharacterActionReference.Jump:
                    nextState = new FireWarriorJumpState();
                    break;
                default:
                    Debug.LogWarning(GamePlayConstraintException.ActionNotPermitted + action);
                    nextState = null;
                    break;
            }
        }
    }
}
