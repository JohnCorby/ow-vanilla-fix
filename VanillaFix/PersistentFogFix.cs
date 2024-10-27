using HarmonyLib;

namespace VanillaFix;

[HarmonyPatch]
public static class PersistentFogFix
{
    [HarmonyPostfix, HarmonyPatch(typeof(PlayerFogWarpDetector), nameof(PlayerFogWarpDetector.OnDisable))]
    public static void PlayerFogWarpDetector_OnDisable(PlayerFogWarpDetector __instance)
    {
        // In the base game method, Mobius only sets _fogFraction to 0; they forget to actually pass that value to the shaders
        // Since it's been disabled, this code never ends up running since its in LateUpdate

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
