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
        }

        public override void OnExit(PowerController powerController)
        {
            
        }
    }
}
