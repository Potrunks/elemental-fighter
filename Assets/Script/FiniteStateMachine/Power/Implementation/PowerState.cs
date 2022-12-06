namespace Assets.Script.FiniteStateMachine
{
    public abstract class PowerState : IPowerState
    {
        public IPowerState nextState;

        public abstract IPowerState CheckingStateModification(PowerController powerController);

        public abstract void OnEnter(PowerController powerController);

        public abstract void OnExit(PowerController powerController);
    }
}
