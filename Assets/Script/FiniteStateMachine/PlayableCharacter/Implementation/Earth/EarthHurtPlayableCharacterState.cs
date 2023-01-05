﻿using Assets.Script.Data;
using Assets.Script.Data.Reference;
using Assets.Script.FiniteStateMachine.PlayableCharacter.Implementation.Fire;
using UnityEngine;

namespace Assets.Script.FiniteStateMachine
{
    public class EarthHurtPlayableCharacterState : PlayableCharacterStateV2
    {
        IPlayableCharacterStateV2 nextState;

        public override IPlayableCharacterStateV2 CheckingStateModification(PlayableCharacterController playableCharacterController)
        {
            if (playableCharacterController.playableCharacterAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
            {
                if (playableCharacterController._currentHealth <= 0)
                {
                    return nextState = new EarthDiePlayableCharacterState();
                }
                return nextState = new EarthIdlePlayableCharacterState();
            }

            if (playableCharacterController._isTouchingByAttack)
            {
                return nextState = new EarthHurtPlayableCharacterState();
            }

            return nextState;
        }

        public override void OnEnter(PlayableCharacterController playableCharacterController)
        {
            playableCharacterController._isTouchingByAttack = false;
            playableCharacterController._bloodEffectForDamage.Play();
            playableCharacterController.playableCharacterAnimator.Play("Hurt", -1, 0f);
            playableCharacterController._audioBusiness.PlayRandomSoundEffect(SoundEffectType.HURTING, playableCharacterController._soundEffectListByType);
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
