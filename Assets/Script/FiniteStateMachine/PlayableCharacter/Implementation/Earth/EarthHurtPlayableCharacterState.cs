using Assets.Script.Data;

namespace Assets.Script.FiniteStateMachine
{
    public class EarthHurtPlayableCharacterState : PlayableCharacterStateV2
    {
        IPlayableCharacterStateV2 nextState;

        public override IPlayableCharacterStateV2 CheckingStateModification(PlayableCharacterController playableCharacterController)
        {
            if (playableCharacterController.playableCharacterAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
            {
                if (playableCharacterController._currentHealth <= 0)
                {
                    return nextState = new EarthDiePlayableCharacterState();
                }
                return nextState = new EarthIdlePlayableCharacterState();
            }

            if (playableCharacterController._isTouchingByAttack)
            {
                return nextState = new EarthHurtPlayableCharacterState();
            }

            return nextState;
        }

        public override void OnEnter(PlayableCharacterController playableCharacterController)
        {
            playableCharacterController.playableCharacterAnimator.Play("Hurt", -1, 0f);
        }

        public override void OnExit(PlayableCharacterController playableCharacterController)
        {
            playableCharacterController._isTouchingByAttack = false;
        }

        public override void PerformingInput(PlayableCharacterActionReference action)
        {
            
        }
    }
}
