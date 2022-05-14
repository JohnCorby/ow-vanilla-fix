using HarmonyLib;

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
    private static bool ProxyBody_Awake(ProxyBody __instance)
    {
        LateInitializerManager.RegisterLateInitializer(__instance);
        GlobalMessenger.AddListener("EnterMapView", __instance.OnEnterMapView);
        GlobalMessenger.AddListener("ExitMapView", __instance.OnExitMapView);

        return false;
    }

    [HarmonyPrefix]
    [HarmonyPatch(typeof(ProxyBody), nameof(ProxyBody.OnDestroy))]
    private static bool ProxyBody_OnDestroy(ProxyBody __instance)
    {
        GlobalMessenger.RemoveListener("EnterMapView", __instance.OnEnterMapView);
        GlobalMessenger.RemoveListener("ExitMapView", __instance.OnExitMapView);
        if (!__instance._initialized) LateInitializerManager.UnregisterLateInitializer(__instance);

        return false;
    }

    [HarmonyPrefix]
    [HarmonyPatch(typeof(ProxyOrbiter), nameof(ProxyOrbiter.Awake))]
    private static bool ProxyOrbiter_Awake(ProxyOrbiter __instance)
    {
        __instance._initialized = false;
        GlobalMessenger.AddListener("EnterMapView", __instance.OnEnterMapView);
        GlobalMessenger.AddListener("ExitMapView", __instance.OnExitMapView);

        return false;
    }

    [HarmonyPrefix]
    [HarmonyPatch(typeof(ProxyOrbiter), nameof(ProxyOrbiter.OnDestroy))]
    private static bool ProxyOrbiter_OnDestroy(ProxyOrbiter __instance)
    {
        GlobalMessenger.RemoveListener("EnterMapView", __instance.OnEnterMapView);
        GlobalMessenger.RemoveListener("ExitMapView", __instance.OnExitMapView);

        return false;
    }
}