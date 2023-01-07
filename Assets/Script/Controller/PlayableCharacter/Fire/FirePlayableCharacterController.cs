using Assets.Script.FiniteStateMachine.PlayableCharacter.Implementation.Fire;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

namespace Assets.Script.Controller.PlayableCharacter.Fire
{
    public class FirePlayableCharacterController : PlayableCharacterController
    {
        [HideInInspector]
        public bool _isHoldingBlock = false;

        [Header("Blocking component")]
        public ParticleSystem _blockVFX;

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
                _isHoldingBlock = true;
            }
            else if (context.canceled)
            {
                _isHoldingBlock = false;
            }
        }
        #endregion
    }
}
