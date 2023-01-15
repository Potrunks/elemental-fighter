using Assets.Script.Data;
using UnityEngine;

namespace Assets.Script.FiniteStateMachine
{
    public class EarthLightAtk2TransitionPlayableCharacterState : PlayableCharacterStateV2
    {
        IPlayableCharacterStateV2 nextState;

        public override IPlayableCharacterStateV2 CheckingStateModification(PlayableCharacterController playableCharacterController)
        {
            if (playableCharacterController._isTouchingByAttack)
            {
                return new EarthHurtPlayableCharacterState();
            }

            if (playableCharacterController.playableCharacterAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
            {
                return new EarthIdlePlayableCharacterState();
            }

            return nextState;
        }

        public override void OnEnter(PlayableCharacterController playableCharacterController)
        {
            playableCharacterController.playableCharacterAnimator.Play("LightATK2Transition");
        }

        public override void OnExit(PlayableCharacterController playableCharacterController)
        {
            playableCharacterController._isTouchingByAttack = false;
        }

        public override void PerformingInput(PlayableCharacterActionReference action)
        {
            switch (action)
            {
                case PlayableCharacterActionReference.MediumAtk:
                    nextState = new EarthMediumAtkPlayableCharacterState();
                    break;
                case PlayableCharacterActionReference.HeavyAtk:
                    nextState = new EarthHeavyAtkPlayableCharacterState();
                    break;
                case PlayableCharacterActionReference.SpecialAtk:
                    nextState = new EarthSpecialAtkPlayableCharacterState();
                    break;
                default:
                    Debug.LogWarning(GamePlayConstraintException.ActionNotPermitted + action);
                    nextState = null;
                    break;
            }
        }
    }
}
