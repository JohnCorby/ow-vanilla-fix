using HarmonyLib;
using System.Collections.Generic;

namespace VanillaFix;

/// <summary>
/// Some GetFact callers don't do null checks.
/// TODO: remove when patch 14 since that fixes this
/// </summary>
[HarmonyPatch]
public static class NewlyRevealFixes
{
	// no im not doing a transpiler BLEHHHHHHHHH
	[HarmonyPrefix]
	[HarmonyPatch(typeof(ShipLogController), nameof(ShipLogController.BuildRevealQueue))]
	private static bool ShipLogController_BuildRevealQueue(ShipLogController __instance, out List<ShipLogFact> __result)
	{
		List<string> list = new List<string>(PlayerData.GetNewlyRevealedFactIDs());
		List<ShipLogFact> list2 = new List<ShipLogFact>();
		for (int i = 0; i < list.Count; i++)
		{
			ShipLogFact fact = __instance._manager.GetFact(list[i]);
			if (fact == null) continue;
			if (fact.HasSource())
			{
				for (int j = i + 1; j < list.Count; j++)
				{
					ShipLogFact fact2 = __instance._manager.GetFact(list[j]);
					if (fact2.GetEntryID().Equals(fact.GetSourceID()) && !fact2.IsRumor())
					{
						list2.Add(fact2);
						list.RemoveAt(j);
						j--;
					}
				}
				list2.Add(fact);
				list.RemoveAt(i);
				i--;
			}
			else
			{
				list2.Add(fact);
				list.RemoveAt(i);
				i--;
				for (int k = i + 1; k < list.Count; k++)
				{
					ShipLogFact fact3 = __instance._manager.GetFact(list[k]);
					if (fact3.GetEntryID().Equals(fact.GetEntryID()) && !fact3.HasSource())
					{
						list2.Add(fact3);
						list.RemoveAt(k);
						k--;
					}
				}
			}
		}
		__result = list2;
		return false;
	}

	[HarmonyPrefix]
	[HarmonyPatch(typeof(ShipLogManager), nameof(ShipLogManager.ClearNewlyRevealedFacts))]
	private static bool ShipLogManager_ClearNewlyRevealedFacts(ShipLogManager __instance)
	{
		List<string> newlyRevealedFactIDs = PlayerData.GetNewlyRevealedFactIDs();
		for (int i = 0; i < newlyRevealedFactIDs.Count; i++)
		{
			__instance.GetFact(newlyRevealedFactIDs[i])?.ClearNewlyRevealed();
		}
		PlayerData.ClearNewlyRevealedFactIDs();
		return false;
	}
}
