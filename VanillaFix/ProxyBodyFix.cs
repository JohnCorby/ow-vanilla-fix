using HarmonyLib;
using UnityEngine;

namespace VanillaFix;

/// <summary>
/// proxy body uses wrong global messenger event with maps,
/// so they don't disappear when you open the map. lol.
/// </summary>
[HarmonyPatch]
public static class ProxyBodyFix
{
	[HarmonyPrefix]
	[HarmonyPatch(typeof(ProxyBody), nameof(ProxyBody.Awake))]
	private static void ProxyBody_Awake(ProxyBody __instance)
	{
		GlobalMessenger.AddListener("EnterMapView", __instance.OnEnterMapView);
		GlobalMessenger.AddListener("ExitMapView", __instance.OnExitMapView);
	}

	[HarmonyPrefix]
	[HarmonyPatch(typeof(ProxyBody), nameof(ProxyBody.OnDestroy))]
	private static void ProxyBody_OnDestroy(ProxyBody __instance)
	{
		GlobalMessenger.RemoveListener("EnterMapView", __instance.OnEnterMapView);
		GlobalMessenger.RemoveListener("ExitMapView", __instance.OnExitMapView);
	}

	[HarmonyPrefix]
	[HarmonyPatch(typeof(ProxyBody), nameof(ProxyBody.OnEnterMapView))]
	private static void ProxyBody_OnEnterMapView(ProxyBody __instance) =>
		__instance._outOfRange = false;

	[HarmonyPrefix]
	[HarmonyPatch(typeof(ProxyOrbiter), nameof(ProxyOrbiter.Awake))]
	private static void ProxyOrbiter_Awake(ProxyOrbiter __instance)
	{
		GlobalMessenger.AddListener("EnterMapView", __instance.OnEnterMapView);
		GlobalMessenger.AddListener("ExitMapView", __instance.OnExitMapView);
	}

	[HarmonyPrefix]
	[HarmonyPatch(typeof(ProxyOrbiter), nameof(ProxyOrbiter.OnDestroy))]
	private static void ProxyOrbiter_OnDestroy(ProxyOrbiter __instance)
	{
		GlobalMessenger.RemoveListener("EnterMapView", __instance.OnEnterMapView);
		GlobalMessenger.RemoveListener("ExitMapView", __instance.OnExitMapView);
	}

	[HarmonyPrefix]
	[HarmonyPatch(typeof(SunProxy), nameof(SunProxy.OnEnterMapView))]
	private static void SunProxy_OnEnterMapView(SunProxy __instance) =>
		__instance.enabled = false;

	[HarmonyPrefix]
	[HarmonyPatch(typeof(SunProxy), nameof(SunProxy.OnExitMapView))]
	private static void SunProxy_OnExitMapView(SunProxy __instance) =>
		__instance.enabled = true;

	[HarmonyPostfix]
	[HarmonyPatch(typeof(SunProxyEffectController), nameof(SunProxyEffectController.UpdateScales))]
	private static void SunProxyEffectController_UpdateScales(SunProxyEffectController __instance, Vector3 atmosphereScale) =>
		__instance._atmosphere.transform.localScale = atmosphereScale;
}
