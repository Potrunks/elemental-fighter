namespace Assets.Script.Business.Tools
{
    public static class AxisModificatorTools
    {
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
