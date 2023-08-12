using HarmonyLib;

namespace VanillaFix;

/// <summary>
/// Some GetFact callers don't do null checks, which causes a softlock.
/// Clear out the null newly revealed facts to fix the problem permanently.
/// TODO: remove when patch 14 since that fixes this
/// </summary>
[HarmonyPatch(typeof(PlayerData))]
public static class NewlyRevealFix
{
	[HarmonyPrefix]
	[HarmonyPatch(nameof(PlayerData.GetNewlyRevealedFactIDs))]
	private static void GetNewlyRevealedFactIDs()
	{
		if (!Locator.GetShipLogManager()) return;

		var modified = false;
		for (var i = PlayerData._currentGameSave.newlyRevealedFactIDs.Count - 1; i >= 0; i--)
		{
			if (Locator.GetShipLogManager().GetFact(PlayerData._currentGameSave.newlyRevealedFactIDs[i]) == null)
			{
				PlayerData._currentGameSave.newlyRevealedFactIDs.RemoveAt(i);
				modified = true;
			}
		}
		if (modified) PlayerData.SaveCurrentGame();
	}
}
