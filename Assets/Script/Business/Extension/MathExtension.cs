namespace Assets.Script.Business.Extension
{
    public static class MathExtension
    {
        /// <summary>
        /// Calculate the value of the percentage of integer value given.
        /// </summary>
        public static float PercentageOf(this int integer, float percentage)
        {
            return integer * percentage / 100;
        }

        /// <summary>
        /// Convert the value to percentage.
        /// </summary>
        public static float ToPercentage(this int integer, float maxValue)
        {
            return integer * 100 / maxValue;
        }
    }
}
