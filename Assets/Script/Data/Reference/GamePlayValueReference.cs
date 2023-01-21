namespace Assets.Script.Data
{
    public static class GamePlayValueReference
    {
        public static bool startDeviceUsingState = false;
        public static float smoothTimeDuringMove = 0.05f;
        public static float velocityLowThreshold = -0.1f;
        public static float velocityHighThreshold = 0.1f;

        /// <summary>
        /// Percentage of health to starting bleeding effect for character.
        /// </summary>
        public static float START_PERCENTAGE_BLEEDING = 80f;

        public static float DASH_FORCE_MULTIPLICATOR = 1.5f;

        /// <summary>
        /// Invincible duration after death during the respawn.
        /// </summary>
        public static int INVINCIBLE_DURATION = 3;
    }
}
