using HarmonyLib;
using UnityEngine;

namespace VanillaFix;

/// <summary>
/// OnDisable sets the fog fraction to 0, but doesnt actually update the fog fade.
/// </summary>
[HarmonyPatch(typeof(PlayerFogWarpDetector))]
public static class PlayerFogFix
{
	/*
	[HarmonyPostfix]
	[HarmonyPatch(nameof(PlayerFogWarpDetector.OnDisable))]
	private static void OnDisable(PlayerFogWarpDetector __instance)
	{
		Mod.Helper.Console.WriteLine("MAKE THE FUCKING FOG FADE ZERo");
		if (__instance._playerEffectBubbleController != null)
			__instance._playerEffectBubbleController.SetFogFade(__instance._fogFraction, __instance._fogColor);
		if (__instance._shipLandingCamEffectBubbleController != null)
			__instance._shipLandingCamEffectBubbleController.SetFogFade(__instance._fogFraction, __instance._fogColor);
	}
	*/
}
