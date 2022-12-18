using Assets.Script.Data.Reference;

namespace Assets.Script.FiniteStateMachine
{
    public class HeavyEarthCastState : PowerState
    {
        public override IPowerState CheckingStateModification(PowerController powerController)
        {
            if (powerController._willBeDestroyed)
            {
                return nextState = new HeavyEarthDestroyState();
            }

            return nextState;
        }

        public override void OnEnter(PowerController powerController)
        {
            powerController._animator.Play("Throwing");
            powerController._audioBusiness.PlayRandomSoundEffect(SoundEffectType.ELEMENTAL_CASTING, powerController._soundEffectByType);
        }

        public override void OnExit(PowerController powerController)
        {
            
        }
    }
}
