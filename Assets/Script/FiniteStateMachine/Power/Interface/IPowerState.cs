namespace Assets.Script.FiniteStateMachine
{
    public interface IPowerState
    {
        IPowerState CheckingStateModification(PowerController powerController);

        void OnEnter(PowerController powerController);

        void OnExit(PowerController powerController);
    }
}
