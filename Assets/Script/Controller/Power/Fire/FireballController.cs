using Assets.Script.FiniteStateMachine.Power.Implementation.Fire;

namespace Assets.Script.Controller.Power.Fire
{
    public class FireballController : PowerController
    {
        #region Monobehaviour Method
        private void Start()
        {
            currentState = new FireballCastState();
            currentState.OnEnter(this);
        }
        #endregion
    }
}
