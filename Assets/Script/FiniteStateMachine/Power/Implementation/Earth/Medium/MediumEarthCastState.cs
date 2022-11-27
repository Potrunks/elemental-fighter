using UnityEngine;

namespace Assets.Script.FiniteStateMachine
{
    public class MediumEarthCastState : PowerState
    {
        public override IPowerState CheckingStateModification(PowerController powerController)
        {
            if (powerController._hasTouchedSomething)
            {
                return nextState = new MediumEarthDestroyState();
            }

            return nextState;
        }

        public override void OnEnter(PowerController powerController)
        {
            powerController._rigidbody.AddForce(powerController.transform.right * (powerController._powerEntity.powerSpeed / 2), ForceMode2D.Impulse);
        }

        public override void OnExit(PowerController powerController)
        {
            
        }
    }
}
