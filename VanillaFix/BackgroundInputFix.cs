using HarmonyLib;
using UnityEngine.InputSystem;

namespace VanillaFix;

/// <summary>
/// patch 13 made it ignore input if the window is not focused.
/// this breaks Run In Background because it makes keys stay held down when they arent.
/// </summary>
[HarmonyPatch(typeof(OWInput))]
public static class BackgroundInputFix
{
	[HarmonyPostfix]
	[HarmonyPatch(nameof(OWInput.Awake))]
	public static void Awake(OWInput __instance) =>
		InputSystem.settings.backgroundBehavior = default;
}
