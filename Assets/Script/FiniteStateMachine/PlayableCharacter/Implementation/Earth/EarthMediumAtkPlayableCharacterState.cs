using Assets.Script.Data;
using Assets.Script.Data.Reference;
using DG.Tweening;

namespace Assets.Script.FiniteStateMachine
{
    public class EarthMediumAtkPlayableCharacterState : PlayableCharacterStateV2
    {
        IPlayableCharacterStateV2 nextState;

        public override IPlayableCharacterStateV2 CheckingStateModification(PlayableCharacterController playableCharacterController)
        {
            if (playableCharacterController._isTouchingByAttack)
            {
                return nextState = new EarthHurtPlayableCharacterState();
            }

            if (playableCharacterController.playableCharacterAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            {
                return nextState = new EarthMediumAtkTransitionPlayableCharacterTransition();
            }

            return nextState;
        }

        public override void OnEnter(PlayableCharacterController playableCharacterController)
        {
            playableCharacterController.playableCharacterMoveSpeed = 0f;
            playableCharacterController.playableCharacterAnimator.Play("MediumATK1");
            playableCharacterController.transform.DOShakePosition(0.4f, strength: 0.1f);
            playableCharacterController._audioBusiness.PlayRandomSoundEffect(SoundEffectType.MELEE_ATTACKING, playableCharacterController._soundEffectListByType);
        }

        public override void OnExit(PlayableCharacterController playableCharacterController)
        {
            playableCharacterController.playableCharacterMoveSpeed = playableCharacterController.playableCharacter.MoveSpeed;
        }

        public override void PerformingInput(PlayableCharacterActionReference action)
        {
            
        }
    }
}
