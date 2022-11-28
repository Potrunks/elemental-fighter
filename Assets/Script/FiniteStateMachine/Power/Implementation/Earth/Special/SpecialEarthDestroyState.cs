using UnityEngine;

namespace Assets.Script.FiniteStateMachine
{
    public class SpecialEarthDestroyState : PowerState
    {
        public override IPowerState CheckingStateModification(PowerController powerController)
        {
            if (powerController._animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
            {
                GameObject.Destroy(powerController.gameObject);
                return nextState = null;
            }

            return nextState;
        }

        public override void OnEnter(PowerController powerController)
        {
            powerController._animator.Play("Destroying");
        }

        public override void OnExit(PowerController powerController)
        {
            
        }
    }
}
