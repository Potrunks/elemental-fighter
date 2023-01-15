using Assets.Script.Controller.PlayableCharacter.Fire;
using Assets.Script.Data;
using Assets.Script.Data.Reference;
using UnityEngine;

namespace Assets.Script.FiniteStateMachine.PlayableCharacter.Implementation.Fire
{
    public class FireWarriorDashState : PlayableCharacterStateV2
    {
        private IPlayableCharacterStateV2 nextState;

        public override IPlayableCharacterStateV2 CheckingStateModification(PlayableCharacterController playableCharacterController)
        {
            if (playableCharacterController._isTouchingByAttack)
            {
                return new FireWarriorHurtState();
            }

            if (playableCharacterController.isGrounding)
            {
                return new FireWarriorIdleState();
            }

            if (playableCharacterController.playableCharacterAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            {
                if (playableCharacterController.isGrounding)
                {
                    return new FireWarriorIdleState();
                }

                return new FireWarriorFallState();
            }

            return nextState;
        }

        public override void OnEnter(PlayableCharacterController playableCharacterController)
        {
            FirePlayableCharacterController character = (FirePlayableCharacterController)playableCharacterController;

            character._nextDashMoveTime = Time.time + character._dashMoveCooldown;
            character.playableCharacterMoveSpeed = 0;
            character.playableCharacterAnimator.Play("DashMove");
            character._audioBusiness.PlayRandomSoundEffect(SoundEffectType.ELEMENTAL_CASTING, character._soundEffectListByType);
            playableCharacterController.playableCharacterRigidbody.AddForce(new Vector2(playableCharacterController.playableCharacter.JumpForce * GamePlayValueReference.DASH_FORCE_MULTIPLICATOR * (playableCharacterController._isLeftFlip ? -1 : 1), 0));
            character._dashVFX.Play();
        }

        public override void OnExit(PlayableCharacterController playableCharacterController)
        {
            playableCharacterController.playableCharacterMoveSpeed = playableCharacterController.playableCharacter.MoveSpeed;
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
