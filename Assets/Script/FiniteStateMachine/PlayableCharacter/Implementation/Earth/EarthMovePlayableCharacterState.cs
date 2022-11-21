using Assets.Script.Data;
using Assets.Script.Data.Reference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Script.FiniteStateMachine
{
    internal class EarthMovePlayableCharacterState : PlayableCharacterStateV2
    {
        private AudioSource moveForwardSound;

        public override IPlayableCharacterStateV2 CheckingStateModification(PlayableCharacterController playableCharacterController)
        {
            if (playableCharacterController.playableCharacterRigidbody.velocity.x <= GamePlayValueReference.velocityXHighThreshold
                && playableCharacterController.playableCharacterRigidbody.velocity.x >= GamePlayValueReference.velocityXLowThreshold)
            {
                return new EarthIdlePlayableCharacterState();
            }

            return null;
        }

        public override void OnEnter(PlayableCharacterController playableCharacterController)
        {
            moveForwardSound = playableCharacterController
                .playableCharacter
                .soundEffectList
                .First(sound => sound.name == "MoveForward")
                .audioSource;

            moveForwardSound.Play();

            playableCharacterController
                .playableCharacterAnimator
                .Play("Run");
        }

        public override void OnExit(PlayableCharacterController playableCharacterController)
        {
            moveForwardSound.Stop();
        }

        public override void PerformingInput(PlayableCharacterActionReference action)
        {
            throw new NotImplementedException();
        }
    }
}
