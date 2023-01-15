namespace Assets.Script.Business.Tools
{
    public static class AxisModificatorTools
    {
        /// <summary>
        /// Calculate a modificator. If player orientation and his own elemental orientation are not in the same sens, the method return -1.
        /// </summary>
        public static int DependCharacterAndElementalOrientation(bool isCharacterFlipLeft, float elementalYRotation)
        {
            int xAxisModificator = 1;
            if ((isCharacterFlipLeft && elementalYRotation == 0)
                || (!isCharacterFlipLeft && elementalYRotation == -1))
            {
                xAxisModificator = -1;
            }
            return xAxisModificator;
        }
    }
}
