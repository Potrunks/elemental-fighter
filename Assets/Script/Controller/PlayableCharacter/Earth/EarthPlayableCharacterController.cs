using Assets.Script.FiniteStateMachine;

namespace Assets.Script.Controller
{
    public class EarthPlayableCharacterController : PlayableCharacterController
    {
        private void Start()
        {
            currentState = new EarthIdlePlayableCharacterState();
            currentState.OnEnter(this);
        }
    }
}
