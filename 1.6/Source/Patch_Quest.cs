using HarmonyLib;
using RimWorld;
using Verse;

namespace RewardMotivationMeme
{
    [HarmonyPatch(typeof(Quest))]
    [HarmonyPatch(nameof(Quest.End))]
    public static class Patch_Quest_End
    {
        public static void Postfix(Quest __instance, QuestEndOutcome outcome)
        {
            if (!__instance.hidden
                && !__instance.initiallyAccepted 
                && !__instance.PartsListForReading.Any(p => p is QuestPart_AddQuest_RefugeeBetrayal) 
                && !__instance.PartsListForReading.Any(p => p is QuestPart_FactionGoodwillChange q && q.change < 0))
            {
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
                foreach (Pawn pawn in PawnsFinder.AllMapsCaravansAndTravellingTransporters_Alive_FreeColonists)
                {
                    if (pawn.Ideo != null && pawn.Ideo.HasPrecept(preceptDef) && pawn.needs.mood != null)
                    {
                        Precept precept = pawn.ideo.Ideo.GetPrecept(preceptDef);
                        pawn.needs.mood.thoughts.memories.TryGainMemory(thoughtDef, null, precept);
                    }
                }
            }
        }
    }
}
