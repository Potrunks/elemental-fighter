using Assets.Script.Data;
using Assets.Script.Data.Reference;
using UnityEngine;

namespace Assets.Script.FiniteStateMachine.PlayableCharacter.Implementation.Fire
{
    public class FireWarriorFallState : PlayableCharacterStateV2
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
                playableCharacterController._audioBusiness.PlayRandomSoundEffect(SoundEffectType.LANDING, playableCharacterController._soundEffectListByType);
                return new FireWarriorIdleState();
            }

            if (nextState != null && nextState.GetType() == typeof(FireWarriorDashState))
            {
                if (playableCharacterController.isDeviceUsed && (playableCharacterController.playableCharacterRigidbody.velocity.x >= GamePlayValueReference.velocityHighThreshold || playableCharacterController.playableCharacterRigidbody.velocity.x <= GamePlayValueReference.velocityLowThreshold))
                {
                    return nextState;
                }
                nextState = null;
            }

            return nextState;
        }

        public override void OnEnter(PlayableCharacterController playableCharacterController)
        {
            playableCharacterController.playableCharacterAnimator.Play("Fall");
        }

        public override void OnExit(PlayableCharacterController playableCharacterController)
        {
            playableCharacterController._isTouchingByAttack = false;
        }

        public override void PerformingInput(PlayableCharacterActionReference action)
        {
            switch (action)
            {
                case PlayableCharacterActionReference.SpecialAtk:
                    nextState = new FireWarriorDashState();
                    break;
                case PlayableCharacterActionReference.HeavyAtk:
                    nextState = new FireWarriorFirstAirBigFireballAttackState();
                    break;
                case PlayableCharacterActionReference.MediumAtk:
                    nextState = new FireWarriorFirstAirFireballAttackState();
                    break;
                case PlayableCharacterActionReference.LightAtk:
                    nextState = new FireWarriorFirstAirSwordAttackState();
                    break;
                default:
                    Debug.LogWarning(GamePlayConstraintException.ActionNotPermitted + action);
                    nextState = null;
                    break;
            }
        }
    }
}
