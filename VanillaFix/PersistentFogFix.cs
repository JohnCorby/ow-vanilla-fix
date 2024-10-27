using HarmonyLib;
using System;
using UnityEngine;

namespace VanillaFix;

[HarmonyPatch]
public static class PersistentFogFix
{
    [HarmonyPostfix, HarmonyPatch(typeof(PlayerFogWarpDetector), nameof(PlayerFogWarpDetector.OnDisable))]
    public static void PlayerFogWarpDetector_OnDisable(PlayerFogWarpDetector __instance)
    {
        // In the base game method, Mobius only sets _fogFraction to 0; they forget to actually pass that value to the shaders
        // Since it's been disabled, this code never ends up running since its in LateUpdate
        SetFogShaders(__instance);
    }

    [HarmonyPrefix, HarmonyPatch(typeof(PlayerFogWarpDetector), nameof(PlayerFogWarpDetector.LateUpdate))]
    public static void PlayerFogWarpDetector_LateUpdate(PlayerFogWarpDetector __instance)
    {
        // Ideal fix is a null check on the line
        // float num2 = this._closestFogWarp.UseFastFogFade() ? 1f : 0.2f;
        // To account for _closestFogWarp being null

        // Doing this instead to not skip the method, it'll still go on to NRE but it'll work
        // Also, this only seems to happen on TH, probably because it has a bramble seed

        // just kidding it actually doesnt NRE here even if you dont return false. i have no idea how

        if (__instance._closestFogWarp == null)
        {
            __instance._fogFraction = 0f;
            SetFogShaders(__instance);
        }
    }

    private static void SetFogShaders(PlayerFogWarpDetector __instance)
    {
        if (__instance._playerEffectBubbleController != null)
        {
            __instance._playerEffectBubbleController.SetFogFade(__instance._fogFraction, __instance._fogColor);
        }
        if (__instance._shipLandingCamEffectBubbleController != null)
        {
            __instance._shipLandingCamEffectBubbleController.SetFogFade(__instance._fogFraction, __instance._fogColor);
        }
    }
}
