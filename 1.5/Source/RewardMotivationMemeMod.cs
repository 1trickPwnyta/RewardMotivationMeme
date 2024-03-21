using HarmonyLib;
using Verse;

namespace RewardMotivationMeme
{
    public class RewardMotivationMemeMod : Mod
    {
        public const string PACKAGE_ID = "rewardmotivationmeme.1trickpwnyta";
        public const string PACKAGE_NAME = "Reward Motivation Meme";

        public RewardMotivationMemeMod(ModContentPack content) : base(content)
        {
            var harmony = new Harmony(PACKAGE_ID);
            harmony.PatchAll();

            Log.Message($"[{PACKAGE_NAME}] Loaded.");
        }
    }
}
