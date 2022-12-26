﻿using Assets.Script.FiniteStateMachine.PlayableCharacter.Implementation.Fire;

namespace Assets.Script.Controller.PlayableCharacter.Fire
{
    public class FirePlayableCharacterController : PlayableCharacterController
    {
        #region MonoBehaviour Method
        private void Start()
        {
            currentState = new FireWarriorIdleState();
            currentState.OnEnter(this);
        }
        #endregion
    }
}