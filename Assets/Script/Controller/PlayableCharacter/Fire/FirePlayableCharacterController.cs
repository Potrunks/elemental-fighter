using Assets.Script.Data;
using Assets.Script.FiniteStateMachine.PlayableCharacter.Implementation.Fire;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

namespace Assets.Script.Controller.PlayableCharacter.Fire
{
    public class FirePlayableCharacterController : PlayableCharacterController
    {
        public int _healthDuringBlockIdle;

        #region MonoBehaviour Method
        private void Start()
        {
            currentState = new FireWarriorIdleState();
            currentState.OnEnter(this);
        }
        #endregion

        #region Action
        public void OnInputBlocking(CallbackContext context)
        {
            if (context.started)
            {
                currentState.PerformingInput(PlayableCharacterActionReference.HOLD_BLOCKING);
            }
            else if (context.canceled)
            {
                currentState.PerformingInput(PlayableCharacterActionReference.RELEASE_BLOCKING);
            }
        }
        #endregion
    }
}
