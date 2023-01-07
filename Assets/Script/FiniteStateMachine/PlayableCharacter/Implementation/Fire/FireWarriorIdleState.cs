using Assets.Script.Controller.PlayableCharacter.Fire;
using Assets.Script.Data;
using UnityEngine;

namespace Assets.Script.FiniteStateMachine.PlayableCharacter.Implementation.Fire
{
    public class FireWarriorIdleState : PlayableCharacterStateV2
    {
        private IPlayableCharacterStateV2 nextState;

        public override IPlayableCharacterStateV2 CheckingStateModification(PlayableCharacterController playableCharacterController)
        {
            if (playableCharacterController._isTouchingByAttack)
            {
                return new FireWarriorHurtState();
            }

            FirePlayableCharacterController controller = (FirePlayableCharacterController)playableCharacterController;

            if (controller._isHoldingBlock)
            {
                return new FireWarriorBlockIdleState();
            }

            if (controller.playableCharacterRigidbody.velocity.y <= GamePlayValueReference.velocityLowThreshold)
            {
                return new FireWarriorFallState();
            }

            if (controller.isDeviceUsed
                && (controller.playableCharacterRigidbody.velocity.x > GamePlayValueReference.velocityHighThreshold
                    || controller.playableCharacterRigidbody.velocity.x < GamePlayValueReference.velocityLowThreshold))
            {
                return new FireWarriorMoveState();
            }

            return nextState;
        }

        public override void OnEnter(PlayableCharacterController playableCharacterController)
        {
            playableCharacterController.playableCharacterMoveSpeed = playableCharacterController.playableCharacter.MoveSpeed;
            playableCharacterController.playableCharacterAnimator.Play("Idle", -1, 0);
        }

        public override void OnExit(PlayableCharacterController playableCharacterController)
        {
            playableCharacterController._isTouchingByAttack = false;
        }

        public override void PerformingInput(PlayableCharacterActionReference action)
        {
            switch (action)
            {
                case PlayableCharacterActionReference.Jump:
                    nextState = new FireWarriorJumpState();
                    break;
                case PlayableCharacterActionReference.LightAtk:
                    nextState = new FireWarriorFirstSwordAttackState();
                    break;
                case PlayableCharacterActionReference.MediumAtk:
                    nextState = new FireWarriorFirstFireballAttackState();
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
