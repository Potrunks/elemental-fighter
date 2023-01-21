using Assets.Script.Data;
using UnityEngine;

namespace Assets.Script.FiniteStateMachine
{
    public class EarthDiePlayableCharacterState : PlayableCharacterStateV2
    {
        private IPlayableCharacterStateV2 nextState;

        public override IPlayableCharacterStateV2 CheckingStateModification(PlayableCharacterController playableCharacterController)
        {
            if (playableCharacterController.playableCharacterAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
            {
                bool gameIsOver = playableCharacterController._characterBusiness.ResetCharacterAfterDeath(playableCharacterController);
                if (!gameIsOver)
                {
                    return new EarthIdlePlayableCharacterState();
                }
            }
            return nextState;
        }

        public override void OnEnter(PlayableCharacterController controller)
        {
            controller.StopCoroutine(controller.DoBleedingCoroutine());
            controller._isBleeding = false;
            controller.playableCharacterMoveSpeed = 0;
            controller.playableCharacterAnimator.Play("Die");
        }

        public override void OnExit(PlayableCharacterController playableCharacterController)
        {
            
        }

        public override void PerformingInput(PlayableCharacterActionReference action)
        {
            switch (action)
            {
                default:
                    Debug.LogWarning(GamePlayConstraintException.ActionNotPermitted + action);
                    nextState = null;
                    break;
            }
        }
    }
}
