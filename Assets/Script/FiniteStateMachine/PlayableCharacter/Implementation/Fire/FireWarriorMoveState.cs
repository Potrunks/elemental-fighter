using Assets.Script.Controller.PlayableCharacter.Fire;
using Assets.Script.Data;
using Assets.Script.Data.Reference;
using UnityEngine;

namespace Assets.Script.FiniteStateMachine.PlayableCharacter.Implementation.Fire
{
    public class FireWarriorMoveState : PlayableCharacterStateV2
    {
        private IPlayableCharacterStateV2 nextState;
        private AudioSource moveSoundEffectPlayed;

        public override IPlayableCharacterStateV2 CheckingStateModification(PlayableCharacterController playableCharacterController)
        {
            if (playableCharacterController._isTouchingByAttack)
            {
                return new FireWarriorHurtState();
            }

            if (nextState != null && nextState.GetType() == typeof(FireWarriorDashState))
            {
                FirePlayableCharacterController character = (FirePlayableCharacterController)playableCharacterController;

                if (character.isDeviceUsed
                    && (character.playableCharacterRigidbody.velocity.x >= GamePlayValueReference.velocityHighThreshold
                        || character.playableCharacterRigidbody.velocity.x <= GamePlayValueReference.velocityLowThreshold)
                    && character._nextDashMoveTime <= Time.time)
                {
                    return nextState;
                }
                nextState = null;
            }

            if (playableCharacterController.playableCharacterRigidbody.velocity.y <= GamePlayValueReference.velocityLowThreshold)
            {
                return new FireWarriorFallState();
            }

            if ((playableCharacterController.playableCharacterRigidbody.velocity.x <= GamePlayValueReference.velocityHighThreshold
                && playableCharacterController.playableCharacterRigidbody.velocity.x >= GamePlayValueReference.velocityLowThreshold) || !playableCharacterController.isDeviceUsed)
            {
                return new FireWarriorIdleState();
            }

            return nextState;
        }

        public override void OnEnter(PlayableCharacterController playableCharacterController)
        {
            playableCharacterController.playableCharacterAnimator.Play("Run");
            moveSoundEffectPlayed = playableCharacterController._audioBusiness.PlayRandomSoundEffect(SoundEffectType.MOVING, playableCharacterController._soundEffectListByType);
        }

        public override void OnExit(PlayableCharacterController playableCharacterController)
        {
            if (moveSoundEffectPlayed != null)
            {
                moveSoundEffectPlayed.Stop();
            }

            playableCharacterController._isTouchingByAttack = false;
        }

        public override void PerformingInput(PlayableCharacterActionReference action)
        {
            switch (action)
            {
                case PlayableCharacterActionReference.SpecialAtk:
                    nextState = new FireWarriorDashState();
                    break;
                case PlayableCharacterActionReference.Jump:
                    nextState = new FireWarriorJumpState();
                    break;
                case PlayableCharacterActionReference.LightAtk:
                    nextState = new FireWarriorFirstSwordAttackState();
                    break;
                case PlayableCharacterActionReference.MediumAtk:
                    nextState = new FireWarriorFirstFireballAttackState();
                    break;
                case PlayableCharacterActionReference.HeavyAtk:
                    nextState = new FireWarriorFirstBigFireballAttackState();
                    break;
                default:
                    Debug.LogWarning(GamePlayConstraintException.ActionNotPermitted + action);
                    nextState = null;
                    break;
            }
        }
    }
}
