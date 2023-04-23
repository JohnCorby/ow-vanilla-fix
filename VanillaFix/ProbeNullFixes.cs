using HarmonyLib;

namespace VanillaFix;

/// <summary>
/// There are probably a few places the game doesn't check if the probe is null,
/// but this will house the most common ones.
/// </summary>
[HarmonyPatch]
public static class ProbeNullFixes
{
	// NomaiWarpStreaming.FixedUpdate calls this without a null check
	[HarmonyPrefix]
	[HarmonyPatch(typeof(SurveyorProbe), nameof(SurveyorProbe.IsLaunched))]
	private static bool SurveyorProbe_IsLaunched(SurveyorProbe __instance, out bool __result)
	{
		__result = __instance && __instance.gameObject.activeSelf;
		return false;
	}
}
