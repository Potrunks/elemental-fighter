using Assets.Script.Data;
using Assets.Script.Data.Reference;
using UnityEngine;

namespace Assets.Script.FiniteStateMachine.PlayableCharacter.Implementation.Fire
{
    public class FireWarriorJumpState : PlayableCharacterStateV2
    {
        private IPlayableCharacterStateV2 nextState;

        public override IPlayableCharacterStateV2 CheckingStateModification(PlayableCharacterController playableCharacterController)
        {
            if (nextState != null)
            {
                return nextState;
            }

            if (playableCharacterController.playableCharacterRigidbody.velocity.y <= GamePlayValueReference.velocityLowThreshold)
            {
                return new FireWarriorFallState();
            }

            return null;
        }

        public override void OnEnter(PlayableCharacterController playableCharacterController)
        {
            playableCharacterController._audioBusiness.PlayRandomSoundEffect(SoundEffectType.JUMPING, playableCharacterController._soundEffectListByType);
            playableCharacterController.playableCharacterRigidbody.AddForce(new Vector2(0f, playableCharacterController.playableCharacter.JumpForce));
            playableCharacterController.playableCharacterAnimator.Play("Jump");
        }

        public override void OnExit(PlayableCharacterController playableCharacterController)
        {
            
        }

        public override void PerformingInput(PlayableCharacterActionReference action)
        {
            
        }
    }
}
