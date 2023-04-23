using HarmonyLib;
using UnityEngine;

namespace VanillaFix;

/// <summary>
/// Prevent the probe from being destroyed when it's anchored to something that's destroyed,
/// fixing (presumably) a plethora of NREs.
///
/// Potentially laggy, idk.
/// </summary>
[HarmonyPatch(typeof(Object))]
public static class ProbeDestroyFix
{
	[HarmonyPostfix]
	[HarmonyPatch(nameof(Object.Destroy), typeof(Object), typeof(float))]
	[HarmonyPatch(nameof(Object.Destroy), typeof(Object))]
	[HarmonyPatch(nameof(Object.DestroyImmediate), typeof(Object), typeof(bool))]
	[HarmonyPatch(nameof(Object.DestroyImmediate), typeof(Object))]
	private static void Destroy(Object obj)
	{
		if (obj is not GameObject gameObject) return;
		var probe = gameObject.GetComponentInChildren<SurveyorProbe>(true);
		if (probe) probe.Unanchor();
	}
}
