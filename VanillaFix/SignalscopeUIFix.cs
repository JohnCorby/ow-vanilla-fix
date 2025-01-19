using HarmonyLib;
using UnityEngine;

namespace VanillaFix;

/// <summary>
/// CREDIT TO SPACEMIKE FOR THIS FIX
///
/// Part of the waveform UI for the signalscope is active at the start of the scene before the
/// signalscope is pulled out and ends up just floating in space before you equip the signalscope.
/// </summary>
[HarmonyPatch(typeof(SignalscopeUI))]
public static class SignalscopeUIFix
{
	[HarmonyPostfix]
	[HarmonyPatch(nameof(SignalscopeUI.Start))]
	private static void Start(SignalscopeUI __instance) => __instance._waveformRenderer.enabled = false;
}
