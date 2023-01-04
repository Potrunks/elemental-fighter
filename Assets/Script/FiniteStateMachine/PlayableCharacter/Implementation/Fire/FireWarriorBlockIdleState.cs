using Assets.Script.Business;
using Assets.Script.Data;
using UnityEngine;

namespace Assets.Script.FiniteStateMachine.PlayableCharacter.Implementation.Fire
{
    public class FireWarriorBlockIdleState : PlayableCharacterStateV2
    {
        private IPlayableCharacterStateV2 nextState;
        private ICharacterBusiness _characterBusiness = new CharacterBusiness();

        private int _healthBeforeBlock;

        public override IPlayableCharacterStateV2 CheckingStateModification(PlayableCharacterController playableCharacterController)
        {
            if (playableCharacterController._isTouchingByAttack)
            {
                playableCharacterController._currentHealth += _characterBusiness.ReturnBlockedDamage(playableCharacterController._currentHealth, _healthBeforeBlock, 3);
                return new FireWarriorBlockingState();
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
            
        }

        public override void PerformingInput(PlayableCharacterActionReference action)
        {
            switch (action)
            {
                case PlayableCharacterActionReference.RELEASE_BLOCKING:
                    nextState = new FireWarriorIdleState();
                    break;
                default:
                    Debug.LogWarning(GamePlayConstraintException.ActionNotPermitted + action);
                    nextState = null;
                    break;
            }
        }
    }
}
