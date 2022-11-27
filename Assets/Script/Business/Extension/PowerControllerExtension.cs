using UnityEngine;

namespace Assets.Script.Business
{
    public static class PowerControllerExtension
    {
        /// <summary>
        /// Check if its time to destruct the power controller.
        /// </summary>
        public static bool isTimeToBeDestruct(this PowerController controller)
        {
            if (controller._selfDestructTimer != 0 && Time.time > controller._destroyLimitTimer)
            {
                return true;
            }
            return false;
        }
    }
}
