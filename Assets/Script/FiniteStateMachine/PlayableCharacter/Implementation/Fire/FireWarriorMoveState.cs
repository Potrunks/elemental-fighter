using Assets.Script.Data;
using Assets.Script.Data.Reference;
using UnityEngine;

namespace Assets.Script.FiniteStateMachine.PlayableCharacter.Implementation.Fire
{
    public class FireWarriorMoveState : PlayableCharacterStateV2
    {
        private IPlayableCharacterStateV2 nextState;
        private AudioSource moveSoundEffectPlayed;

        public override IPlayableCharacterStateV2 CheckingStateModification(PlayableCharacterController playableCharacterController)
        {
            if (nextState != null)
            {
                return nextState;
            }

            if (playableCharacterController.playableCharacterRigidbody.velocity.x <= GamePlayValueReference.velocityHighThreshold
                && playableCharacterController.playableCharacterRigidbody.velocity.x >= GamePlayValueReference.velocityLowThreshold)
            {
                return new FireWarriorIdleState();
            }

            return null;
        }

        public override void OnEnter(PlayableCharacterController playableCharacterController)
        {
            playableCharacterController.playableCharacterAnimator.Play("Run");
            moveSoundEffectPlayed = playableCharacterController._audioBusiness.PlayRandomSoundEffect(SoundEffectType.MOVING, playableCharacterController._soundEffectListByType);
        }

        public override void OnExit(PlayableCharacterController playableCharacterController)
        {
            if (moveSoundEffectPlayed != null)
            {
                moveSoundEffectPlayed.Stop();
            }
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
