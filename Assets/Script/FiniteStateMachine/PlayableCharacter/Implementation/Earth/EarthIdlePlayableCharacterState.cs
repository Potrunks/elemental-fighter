using Assets.Script.Data;
using UnityEngine;

namespace Assets.Script.FiniteStateMachine
{
    public class EarthIdlePlayableCharacterState : PlayableCharacterStateV2
    {
        IPlayableCharacterStateV2 nextState;

        public override IPlayableCharacterStateV2 CheckingStateModification(PlayableCharacterController playableCharacterController)
        {
            if (playableCharacterController._isTouchingByAttack)
            {
                return new EarthHurtPlayableCharacterState();
            }

            if (playableCharacterController.isDeviceUsed
                && (playableCharacterController.playableCharacterRigidbody.velocity.x > GamePlayValueReference.velocityHighThreshold
                    || playableCharacterController.playableCharacterRigidbody.velocity.x < GamePlayValueReference.velocityLowThreshold))
            {
                return new EarthMovePlayableCharacterState();
            }
            return nextState;
        }

        public override void OnEnter(PlayableCharacterController playableCharacterController)
        {
            playableCharacterController.playableCharacterMoveSpeed = playableCharacterController.playableCharacter.MoveSpeed;
            playableCharacterController.playableCharacterAnimator.Play("Idle");
        }

        public override void OnExit(PlayableCharacterController playableCharacterController)
        {

        }

        public override void PerformingInput(PlayableCharacterActionReference action)
        {
            switch (action)
            {
                case PlayableCharacterActionReference.Jump:
                    nextState = new EarthJumpPlayableCharacterState();
                    break;
                case PlayableCharacterActionReference.MediumAtk:
                    nextState = new EarthMediumAtkPlayableCharacterState();
                    break;
                case PlayableCharacterActionReference.LightAtk:
                    nextState = new EarthLightAtkPlayableCharacterState();
                    break;
                case PlayableCharacterActionReference.HeavyAtk:
                    nextState = new EarthHeavyAtkPlayableCharacterState();
                    break;
                case PlayableCharacterActionReference.SpecialAtk:
                    nextState = new EarthSpecialAtkPlayableCharacterState();
                    break;
                case PlayableCharacterActionReference.SpecialAtk2:
                    nextState = new EarthSpecialAtk2PlayableCharacterState();
                    break;
                default:
                    Debug.LogWarning(GamePlayConstraintException.ActionNotPermitted + action);
                    nextState = null;
                    break;
            }
        }
    }
}
