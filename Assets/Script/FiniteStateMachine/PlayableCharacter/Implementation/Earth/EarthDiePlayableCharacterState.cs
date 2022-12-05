using Assets.Script.Data.Reference;

namespace Assets.Script.FiniteStateMachine
{
    public class EarthDiePlayableCharacterState : PlayableCharacterStateV2
    {
        private IPlayableCharacterStateV2 nextState;

        public override IPlayableCharacterStateV2 CheckingStateModification(PlayableCharacterController playableCharacterController)
        {
            if (playableCharacterController.playableCharacterAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
            {
                bool gameIsOver = playableCharacterController.characterBusiness.ResetCharacterAfterDeath(playableCharacterController);
                if (!gameIsOver)
                {
                    return nextState = new EarthIdlePlayableCharacterState();
                }
            }
            return nextState;
        }

        public override void OnEnter(PlayableCharacterController playableCharacterController)
        {
            playableCharacterController.playableCharacterAnimator.Play("Die");
        }

        public override void OnExit(PlayableCharacterController playableCharacterController)
        {
            
        }

        public override void PerformingInput(PlayableCharacterActionReference action)
        {
            
        }
    }
}
