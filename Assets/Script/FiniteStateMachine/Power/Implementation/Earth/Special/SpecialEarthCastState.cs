namespace Assets.Script.FiniteStateMachine
{
    public class SpecialEarthCastState : PowerState
    {
        public override IPowerState CheckingStateModification(PowerController powerController)
        {
            return null;
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
