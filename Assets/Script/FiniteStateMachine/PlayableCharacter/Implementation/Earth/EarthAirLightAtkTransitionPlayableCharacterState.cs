using Assets.Script.Data;
using Assets.Script.Data.Reference;

namespace Assets.Script.FiniteStateMachine
{
    public class EarthAirLightAtkTransitionPlayableCharacterState : PlayableCharacterStateV2
    {
        IPlayableCharacterStateV2 nextState;

        public override IPlayableCharacterStateV2 CheckingStateModification(PlayableCharacterController playableCharacterController)
        {
            if (playableCharacterController._isTouchingByAttack)
            {
                return nextState = new EarthHurtPlayableCharacterState();
            }

            if (playableCharacterController.isGrounding)
            {
                return nextState = new EarthIdlePlayableCharacterState();
            }

            if (playableCharacterController.playableCharacterAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
            {
                if (playableCharacterController.playableCharacterRigidbody.velocity.y >= GamePlayValueReference.velocityHighThreshold)
                {
                    return nextState = new EarthJumpPlayableCharacterState();
                }
                if (playableCharacterController.playableCharacterRigidbody.velocity.y <= GamePlayValueReference.velocityLowThreshold)
                {
                    return nextState = new EarthFallPlayableCharacterState();
                }
            }

            return nextState;
        }

        public override void OnEnter(PlayableCharacterController playableCharacterController)
        {
            playableCharacterController.playableCharacterAnimator.Play("AirAttack1Transition");
        }

        public override void OnExit(PlayableCharacterController playableCharacterController)
        {
            
        }

        public override void PerformingInput(PlayableCharacterActionReference action)
        {
            
        }
    }
}
