using Assets.Script.Data;
using Assets.Script.Data.Reference;

namespace Assets.Script.FiniteStateMachine
{
    public class EarthHurtPlayableCharacterState : PlayableCharacterStateV2
    {
        IPlayableCharacterStateV2 nextState;
        private float? _normalizedTimeOfAnimation = null;

        public override IPlayableCharacterStateV2 CheckingStateModification(PlayableCharacterController playableCharacterController)
        {
            _normalizedTimeOfAnimation = playableCharacterController.playableCharacterAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime;

            if (_normalizedTimeOfAnimation == 0f)
            {
                playableCharacterController._bloodEffectForDamage.Play();
            }

            if (_normalizedTimeOfAnimation >= 1f)
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
            playableCharacterController.playableCharacterAnimator.Play("Hurt", -1, 0f);
            playableCharacterController._audioBusiness.PlayRandomSoundEffect(SoundEffectType.HURTING, playableCharacterController._soundEffectListByType);
        }

        public override void OnExit(PlayableCharacterController playableCharacterController)
        {
            playableCharacterController._isTouchingByAttack = false;
        }

        public override void PerformingInput(PlayableCharacterActionReference action)
        {
            
        }
    }
}
