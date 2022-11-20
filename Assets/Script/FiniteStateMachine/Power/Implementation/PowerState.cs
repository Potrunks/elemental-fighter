using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Script.FiniteStateMachine
{
    public abstract class PowerState : IPowerState
    {
        public abstract IPowerState CheckingStateModification(PowerController powerController);

        public abstract void OnEnter(PowerController powerController);

        public abstract void OnExit(PowerController powerController);
    }
}
