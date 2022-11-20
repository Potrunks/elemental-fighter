using Assets.Script.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Script.FiniteStateMachine
{
    public class EarthCastState : PowerState
    {
        public override IPowerState CheckingStateModification(PowerController powerController)
        {
            throw new NotImplementedException();
        }

        public override void OnEnter(PowerController powerController)
        {
            powerController.elementalBusiness.RockOutOfGround(powerController);
        }

        public override void OnExit(PowerController powerController)
        {
            throw new NotImplementedException();
        }
    }
}
