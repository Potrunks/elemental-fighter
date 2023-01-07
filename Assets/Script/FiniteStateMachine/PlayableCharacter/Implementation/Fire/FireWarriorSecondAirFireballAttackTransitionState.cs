using Assets.Script.Data;
using UnityEngine;

namespace Assets.Script.FiniteStateMachine.PlayableCharacter.Implementation.Fire
{
    public class FireWarriorSecondAirFireballAttackTransitionState : PlayableCharacterStateV2
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
                if (playableCharacterController.playableCharacterRigidbody.velocity.y >= GamePlayValueReference.velocityHighThreshold)
                {
                    return new FireWarriorJumpState();
                }

                if (playableCharacterController.playableCharacterRigidbody.velocity.y <= GamePlayValueReference.velocityLowThreshold)
                {
                    return new FireWarriorFallState();
                }
            }

            return nextState;
        }

        public override void OnEnter(PlayableCharacterController playableCharacterController)
        {
            playableCharacterController.playableCharacterAnimator.Play("AirMediumAttack2Transition");
        }

        public override void OnExit(PlayableCharacterController playableCharacterController)
        {
            playableCharacterController._isTouchingByAttack = false;
        }

        public override void PerformingInput(PlayableCharacterActionReference action)
        {
            switch (action)
            {
                case PlayableCharacterActionReference.HeavyAtk:
                    nextState = new FireWarriorFirstAirBigFireballAttackState();
                    break;
                default:
                    Debug.LogWarning(GamePlayConstraintException.ActionNotPermitted + action);
                    nextState = null;
                    break;
            }
        }
    }
}
