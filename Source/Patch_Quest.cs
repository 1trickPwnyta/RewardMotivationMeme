using HarmonyLib;
using RimWorld;
using System;
using Verse;

namespace RewardMotivationMeme
{
    [HarmonyPatch(typeof(Quest))]
    [HarmonyPatch(nameof(Quest.End))]
    public static class Patch_Quest_End
    {
        public static void Postfix(Quest __instance, QuestEndOutcome outcome)
        {
            Debug.Log(__instance.name);
            Debug.Log(__instance.description);
            Debug.Log(outcome.ToString());

            // Exceptions
            if (__instance.name == "RefugeePodCrash")
            {
                return;
            }
            if (__instance.name == "WandererJoins")
            {
                return;
            }
            if (__instance.name == "WandererJoinAbasia")
            {
                return;
            }
            if (__instance.name == "The Deserter")
            {
                return;
            }
            if (__instance.name.Contains(" Work Site "))
            {
                return;
            }
            if (__instance.name.Contains("Betrayal Offer"))
            {
                return;
            }
            if (__instance.description != null && __instance.description.ToString().Contains("long-range mineral scanner"))
            {
                return;
            }
            if (__instance.description != null && __instance.description.ToString().Contains(" begging "))
            {
                return;
            }
            if (__instance.description != null && __instance.description.ToString().Contains("desperate refugee"))
            {
                return;
            }

            // Create the thought based on the quest outcome
            ThoughtDef thoughtDef;
            if (outcome != QuestEndOutcome.Fail)
            {
                thoughtDef = ThoughtDef.Named("RewardMotivationMeme_QuestCompleted");
            }
            else
            {
                thoughtDef = ThoughtDef.Named("RewardMotivationMeme_QuestFailed");
            }

            // Give the thought to every colonist with the precept
            PreceptDef preceptDef = DefDatabase<PreceptDef>.GetNamed("RewardMotivationMeme_QuestsMandatory");
            foreach (Pawn pawn in PawnsFinder.AllMapsCaravansAndTravelingTransportPods_Alive_FreeColonists)
            {
                if (pawn.ideo.Ideo.HasPrecept(preceptDef) && pawn.needs.mood != null)
                {
                    Precept precept = pawn.ideo.Ideo.GetPrecept(preceptDef);
                    pawn.needs.mood.thoughts.memories.TryGainMemory(thoughtDef, null, precept);
                }
            }
        }
    }
}
