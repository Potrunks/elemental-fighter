using Assets.Script.Data.Reference;

namespace Assets.Script.FiniteStateMachine
{
    public class EarthIdlePlayableCharacterState : PlayableCharacterStateV2
    {
        public override IPlayableCharacterStateV2 CheckingStateModification(PlayableCharacterController playableCharacterController)
        {
            throw new System.NotImplementedException();
        }

        public override void OnEnter(PlayableCharacterController playableCharacterController)
        {
            playableCharacterController.playableCharacterAnimator.Play("Idle");
        }

        public override void OnExit(PlayableCharacterController playableCharacterController)
        {
            throw new System.NotImplementedException();
        }

        public override void PerformingInput(PlayableCharacterActionReference action)
        {
            throw new System.NotImplementedException();
        }
    }
}
