using HarmonyLib;

namespace VanillaFix;

/// <summary>
/// There are probably a few places the game doesn't check if the probe is null,
/// but this will house the most common ones.
/// </summary>
public static class ProbeNullFixes
{
	[HarmonyPrefix]
	[HarmonyPatch(nameof(SurveyorProbe.IsLaunched))]
	private static bool SurveyorProbe_IsLaunched(SurveyorProbe __instance, out bool __result)
	{
		__result = __instance && __instance.gameObject.activeSelf;
		return false;
	}
}
