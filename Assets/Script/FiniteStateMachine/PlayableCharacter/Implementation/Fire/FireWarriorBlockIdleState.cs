using Assets.Script.Controller.PlayableCharacter.Fire;
using Assets.Script.Data;
using UnityEngine;

namespace Assets.Script.FiniteStateMachine.PlayableCharacter.Implementation.Fire
{
    public class FireWarriorBlockIdleState : PlayableCharacterStateV2
    {
        private IPlayableCharacterStateV2 nextState;

        private int _healthBeforeBlock;

        public override IPlayableCharacterStateV2 CheckingStateModification(PlayableCharacterController playableCharacterController)
        {
            if (playableCharacterController._isTouchingByAttack)
            {
                return new FireWarriorBlockingState(_healthBeforeBlock);
            }

            FirePlayableCharacterController controller = (FirePlayableCharacterController)playableCharacterController;
            if (!controller._isHoldingBlock)
            {
                return new FireWarriorIdleState();
            }

            return nextState;
        }

        public override void OnEnter(PlayableCharacterController playableCharacterController)
        {
            playableCharacterController.playableCharacterMoveSpeed = 0;
            _healthBeforeBlock = playableCharacterController._currentHealth;
            playableCharacterController.playableCharacterAnimator.Play("BlockIdle");
        }

        public override void OnExit(PlayableCharacterController playableCharacterController)
        {
            playableCharacterController._isTouchingByAttack = false;
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
