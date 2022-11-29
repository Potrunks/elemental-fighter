namespace Assets.Script.FiniteStateMachine
{
    public class SpecialEarthCastState : PowerState
    {
        public override IPowerState CheckingStateModification(PowerController powerController)
        {
            if (powerController._willBeDestroyed)
            {
                return nextState = new SpecialEarthDestroyState();
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
