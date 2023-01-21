using Assets.Script.Data;
using Assets.Script.Data.Reference;
using UnityEngine;

namespace Assets.Script.FiniteStateMachine.PlayableCharacter.Implementation.Fire
{
    public class FireWarriorDieState : PlayableCharacterStateV2
    {
        private IPlayableCharacterStateV2 nextState;

        public override IPlayableCharacterStateV2 CheckingStateModification(PlayableCharacterController playableCharacterController)
        {
            if (playableCharacterController.playableCharacterAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            {
                bool gameIsOver = playableCharacterController._characterBusiness.ResetCharacterAfterDeath(playableCharacterController);
                if (!gameIsOver)
                {
                    return new FireWarriorIdleState();
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
            controller._audioBusiness.PlayRandomVoice(VoiceType.DIE, controller._voiceListByType);
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
