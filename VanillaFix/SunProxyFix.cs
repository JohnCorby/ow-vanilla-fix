using HarmonyLib;
using UnityEngine;

namespace VanillaFix;

/// <summary>
/// CREDIT TO MEGAPIGGY FOR THIS FIX
/// for some reason they do atmosphereScale * 2 in base game, which makes the atmosphere not show up
/// </summary>
[HarmonyPatch(typeof(SunProxyEffectController))]
public static class SunProxyFix
{
	[HarmonyPostfix]
	[HarmonyPatch(nameof(SunProxyEffectController.UpdateScales))]
	private static void UpdateScales(SunProxyEffectController __instance, Vector3 atmosphereScale)
	{
		if (__instance._atmosphere)
			__instance._atmosphere.transform.localScale = atmosphereScale;
	}
}
