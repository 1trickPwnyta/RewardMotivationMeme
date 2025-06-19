namespace RewardMotivationMeme
{
    public static class Debug
    {
        public static void Log(string message)
        {
#if DEBUG
            Verse.Log.Message($"[{RewardMotivationMemeMod.PACKAGE_NAME}] {message}");
#endif
        }
    }
}
