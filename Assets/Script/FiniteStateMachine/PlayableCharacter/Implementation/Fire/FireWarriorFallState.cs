using Assets.Script.Data;
using Assets.Script.Data.Reference;

namespace Assets.Script.FiniteStateMachine.PlayableCharacter.Implementation.Fire
{
    public class FireWarriorFallState : PlayableCharacterStateV2
    {
        private IPlayableCharacterStateV2 nextState;

        public override IPlayableCharacterStateV2 CheckingStateModification(PlayableCharacterController playableCharacterController)
        {
            if (nextState != null)
            {
                return nextState;
            }

            if (playableCharacterController.isGrounding)
            {
                playableCharacterController._audioBusiness.PlayRandomSoundEffect(SoundEffectType.LANDING, playableCharacterController._soundEffectListByType);
                return new FireWarriorIdleState();
            }

            return null;
        }

        public override void OnEnter(PlayableCharacterController playableCharacterController)
        {
            playableCharacterController.playableCharacterAnimator.Play("Fall");
        }

        public override void OnExit(PlayableCharacterController playableCharacterController)
        {
            
        }

        public override void PerformingInput(PlayableCharacterActionReference action)
        {
            
        }
    }
}
