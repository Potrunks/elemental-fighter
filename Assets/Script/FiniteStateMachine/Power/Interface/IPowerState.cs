using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Script.FiniteStateMachine
{
    public interface IPowerState
    {
        IPowerState CheckingStateModification(PowerController powerController);

        void OnEnter(PowerController powerController);

        void OnExit(PowerController powerController);
    }
}
