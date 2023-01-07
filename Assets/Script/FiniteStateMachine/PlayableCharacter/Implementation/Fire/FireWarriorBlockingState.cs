using Assets.Script.Business;
using Assets.Script.Controller.PlayableCharacter.Fire;
using Assets.Script.Data;
using Assets.Script.Data.Reference;
using UnityEngine;

namespace Assets.Script.FiniteStateMachine.PlayableCharacter.Implementation.Fire
{
    public class FireWarriorBlockingState : PlayableCharacterStateV2
    {
        private IPlayableCharacterStateV2 nextState;
        private ICharacterBusiness _characterBusiness = new CharacterBusiness();
        private int _healthBeforeBlock;

        public FireWarriorBlockingState(int healthBeforeBlock)
        {
            _healthBeforeBlock = healthBeforeBlock;
        }

        public override IPlayableCharacterStateV2 CheckingStateModification(PlayableCharacterController playableCharacterController)
        {
            FirePlayableCharacterController controller = (FirePlayableCharacterController)playableCharacterController;
            if (controller._isTouchingByAttack)
            {
                if (controller._isHoldingBlock)
                {
                    return new FireWarriorBlockingState(_healthBeforeBlock);
                }
                else
                {
                    return new FireWarriorHurtState();
                }
            }

            if (controller.playableCharacterAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            {
                if (controller._currentHealth <= 0)
                {
                    return new FireWarriorDieState();
                }
                else
                {
                    if (controller._isHoldingBlock)
                    {
                        return new FireWarriorBlockIdleState();
                    }
                    else
                    {
                        return new FireWarriorIdleState();
                    }
                }
            }

            return nextState;
        }

        public override void OnEnter(PlayableCharacterController playableCharacterController)
        {
            FirePlayableCharacterController character = (FirePlayableCharacterController)playableCharacterController;
            character._blockVFX.Play();
            character.playableCharacterAnimator.Play("Blocking", -1, 0);
            character._audioBusiness.PlayRandomSoundEffect(SoundEffectType.MELEE_BLOCKING, character._soundEffectListByType);
            character._currentHealth += _characterBusiness.ReturnBlockedDamage(character._currentHealth, _healthBeforeBlock, 3);
            _healthBeforeBlock = character._currentHealth;
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
