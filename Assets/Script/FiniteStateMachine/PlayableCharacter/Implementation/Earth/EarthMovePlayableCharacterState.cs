using Assets.Script.Data;
using System.Linq;
using UnityEngine;

namespace Assets.Script.FiniteStateMachine
{
    public class EarthMovePlayableCharacterState : PlayableCharacterStateV2
    {
        IPlayableCharacterStateV2 nextState;
        private AudioSource soundEffect;

        public override IPlayableCharacterStateV2 CheckingStateModification(PlayableCharacterController playableCharacterController)
        {
            if (playableCharacterController._isTouchingByAttack)
            {
                return new EarthHurtPlayableCharacterState();
            }

            if (playableCharacterController.playableCharacterRigidbody.velocity.y <= GamePlayValueReference.velocityLowThreshold)
            {
                return new EarthFallPlayableCharacterState();
            }

            if (playableCharacterController.playableCharacterRigidbody.velocity.x <= GamePlayValueReference.velocityHighThreshold
                && playableCharacterController.playableCharacterRigidbody.velocity.x >= GamePlayValueReference.velocityLowThreshold)
            {
                return new EarthIdlePlayableCharacterState();
            }
            return nextState;
        }

        public override void OnEnter(PlayableCharacterController playableCharacterController)
        {
            playableCharacterController
                .playableCharacterAnimator
                .Play("Run");

            if (playableCharacterController.playableCharacter.SoundEffectList.Any())
            {
                soundEffect = playableCharacterController
                .playableCharacter
                .SoundEffectList
                .First(sound => sound.name == "MoveForward")
                .audioSource;

                soundEffect.Play();
            }
        }

        public override void OnExit(PlayableCharacterController playableCharacterController)
        {
            if (soundEffect != null)
            {
                soundEffect.Stop();
            }
        }

        public override void PerformingInput(PlayableCharacterActionReference action)
        {
            switch (action)
            {
                case PlayableCharacterActionReference.Jump:
                    nextState = new EarthJumpPlayableCharacterState();
                    break;
                case PlayableCharacterActionReference.MediumAtk:
                    nextState = new EarthMediumAtkPlayableCharacterState();
                    break;
                case PlayableCharacterActionReference.LightAtk:
                    nextState = new EarthLightAtkPlayableCharacterState();
                    break;
                case PlayableCharacterActionReference.HeavyAtk:
                    nextState = new EarthHeavyAtkPlayableCharacterState();
                    break;
                case PlayableCharacterActionReference.SpecialAtk:
                    nextState = new EarthSpecialAtkPlayableCharacterState();
                    break;
                case PlayableCharacterActionReference.SpecialAtk2:
                    nextState = new EarthSpecialAtk2PlayableCharacterState();
                    break;
                default:
                    Debug.LogWarning(GamePlayConstraintException.ActionNotPermitted + action);
                    nextState = null;
                    break;
            }
        }
    }
}
