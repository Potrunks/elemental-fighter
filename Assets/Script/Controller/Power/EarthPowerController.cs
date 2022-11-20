using Assets.Script.Business;
using Assets.Script.Business.Implementation;
using Assets.Script.FiniteStateMachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Script.Controller
{
    public class EarthPowerController : PowerController
    {
        private void Start()
        {
            powerState = new EarthCastState();
            powerState.OnEnter(this);
        }
    }
}
