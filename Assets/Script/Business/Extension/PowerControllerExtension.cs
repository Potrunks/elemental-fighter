using UnityEngine;

namespace Assets.Script.Business
{
    public static class PowerControllerExtension
    {
        /// <summary>
        /// Check if its time to destruct the power controller.
        /// </summary>
        public static bool IsTimeToBeDestruct(this PowerController controller)
        {
            if (controller._selfDestructTimer != 0 && Time.time > controller._destroyLimitTimer)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Trigger self destruct of the power controller and his GameObject attached.
        /// </summary>
        public static void TriggerSelfDestruct(this PowerController power, float selfDestructTimer)
        {
            power._selfDestructTimer = selfDestructTimer;
            power._destroyLimitTimer = Time.time + power._selfDestructTimer;
        }
    }
}
