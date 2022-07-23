using HarmonyLib;
using OWML.Utils;
using UnityEngine;

namespace VanillaFix;

[HarmonyPatch(typeof(QuantumMoon))]
public static class QuantumMoonFix
{
	[HarmonyPrefix]
	[HarmonyPatch(nameof(QuantumMoon.OnDisable))]
	public static void OnDisablePatch(QuantumMoon __instance)
	{
		//Original OnDisable:		this._outerCloudTransform.gameObject.SetActive(false);
		//Problem: this does not check if _outerCloudTransform is null before trying to disable its gameObject
		//Solution: if _outerCloudTransform is null, create a new dummy GameObject to sate OnDisable's appetite
		Transform outerCloudTransform = __instance.GetValue<Transform>("_outerCloudTransform");
		if (outerCloudTransform == null)
			__instance.SetValue("_outerCloudTransform", new GameObject().transform);
	}
}