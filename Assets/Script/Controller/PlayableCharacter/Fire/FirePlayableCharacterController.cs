using Assets.Script.FiniteStateMachine.PlayableCharacter.Implementation.Fire;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Script.Controller.PlayableCharacter.Fire
{
    public class FirePlayableCharacterController : PlayableCharacterController
    {
        [Header("Blocking component")]
        public ParticleSystem _blockVFX;
        [HideInInspector]
        public bool _isHoldingBlock = false;

        [Header("Dash component")]
        public ParticleSystem _dashVFX;
        public float _dashMoveCooldown;
        [HideInInspector]
        public float _nextDashMoveTime = 0;

        #region MonoBehaviour Method
        private void Start()
        {
            currentState = new FireWarriorIdleState();
            currentState.OnEnter(this);
        }
        #endregion

        #region Action
        public void OnInputBlocking(InputAction.CallbackContext context)
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
