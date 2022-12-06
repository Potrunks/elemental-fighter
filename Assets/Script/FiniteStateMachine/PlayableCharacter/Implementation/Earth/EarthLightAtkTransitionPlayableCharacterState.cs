using Assets.Script.Data;
using UnityEngine;

namespace Assets.Script.FiniteStateMachine
{
    public class EarthLightAtkTransitionPlayableCharacterState : PlayableCharacterStateV2
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
            playableCharacterController.playableCharacterAnimator.Play("LightATK1Transition");
        }

        public override void OnExit(PlayableCharacterController playableCharacterController)
        {
            
        }

        public override void PerformingInput(PlayableCharacterActionReference action)
        {
            switch (action)
            {
                case PlayableCharacterActionReference.LightAtk:
                    nextState = new EarthLightAtk2PlayableCharacterState();
                    break;
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
