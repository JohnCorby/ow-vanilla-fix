using HarmonyLib;
using UnityEngine.InputSystem;

namespace VanillaFix;

/// <summary>
/// patch 13 made it so devices arent reset on lost focus.
/// this breaks runInBackground because it makes keys stay held down when they arent.
/// </summary>
[HarmonyPatch(typeof(OWInput))]
public static class BackgroundInputFix
{
	[HarmonyPostfix]
	[HarmonyPatch(nameof(OWInput.Awake))]
	private static void Awake(OWInput __instance) =>
		InputSystem.settings.backgroundBehavior = InputSettings.BackgroundBehavior.ResetAndDisableAllDevices;
}
