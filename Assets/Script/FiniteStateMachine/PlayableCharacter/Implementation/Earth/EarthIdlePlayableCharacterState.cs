using Assets.Script.Data;
using Assets.Script.Data.Reference;

namespace Assets.Script.FiniteStateMachine
{
    public class EarthIdlePlayableCharacterState : PlayableCharacterStateV2
    {
        public override IPlayableCharacterStateV2 CheckingStateModification(PlayableCharacterController playableCharacterController)
        {
            if (playableCharacterController.isDeviceUsed
                && (playableCharacterController.playableCharacterRigidbody.velocity.x > GamePlayValueReference.velocityXHighThreshold
                    || playableCharacterController.playableCharacterRigidbody.velocity.x < GamePlayValueReference.velocityXLowThreshold))
            {
                return new EarthMovePlayableCharacterState();
            }
            return null;
        }

        public override void OnEnter(PlayableCharacterController playableCharacterController)
        {
            playableCharacterController.playableCharacterAnimator.Play("Idle");
        }

        public override void OnExit(PlayableCharacterController playableCharacterController)
        {
        }

        public override void PerformingInput(PlayableCharacterActionReference action)
        {
        }
    }
}
