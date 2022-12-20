using Assets.Script.Data;
using Assets.Script.Data.Reference;

namespace Assets.Script.FiniteStateMachine
{
    public class EarthAirLightAtkPlayableCharacterState : PlayableCharacterStateV2
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
                nextState = new EarthAirLightAtkTransitionPlayableCharacterState();
            }

            return nextState;
        }

        public override void OnEnter(PlayableCharacterController playableCharacterController)
        {
            playableCharacterController.playableCharacterAnimator.Play("AirAttack1");
            playableCharacterController._audioBusiness.PlayRandomSoundEffect(SoundEffectType.MELEE_ATTACKING, playableCharacterController._soundEffectListByType);
        }

        public override void OnExit(PlayableCharacterController playableCharacterController)
        {
            
        }

        public override void PerformingInput(PlayableCharacterActionReference action)
        {
            
        }
    }
}
