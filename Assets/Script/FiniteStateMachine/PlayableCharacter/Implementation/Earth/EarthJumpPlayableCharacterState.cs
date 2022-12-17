using Assets.Script.Data;
using Assets.Script.Data.Reference;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Script.FiniteStateMachine
{
    public class EarthJumpPlayableCharacterState : PlayableCharacterStateV2
    {
        IPlayableCharacterStateV2 nextState;

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

            return nextState;
        }

        public override void OnEnter(PlayableCharacterController playableCharacterController)
        {
            playableCharacterController.playableCharacterRigidbody.AddForce(new Vector2(0f, playableCharacterController.playableCharacter.JumpForce));

            playableCharacterController.playableCharacterAnimator.Play("Jump");

            List<AudioSource> audioSourceList = playableCharacterController._soundEffectListByType != null ? playableCharacterController._soundEffectListByType[SoundEffectType.JUMPING] : null;
            playableCharacterController._audioBusiness.PlayRandomAudioSource(audioSourceList);
        }

        public override void OnExit(PlayableCharacterController playableCharacterController)
        {
            
        }

        public override void PerformingInput(PlayableCharacterActionReference action)
        {
            switch (action)
            {
                case PlayableCharacterActionReference.LightAtk:
                    nextState = new EarthAirLightAtkPlayableCharacterState();
                    break;
                default:
                    Debug.LogWarning(GamePlayConstraintException.ActionNotPermitted + action);
                    nextState = null;
                    break;
            }
        }
    }
}
