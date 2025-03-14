using HarmonyLib;
using UnityEngine;

namespace VanillaFix;

[HarmonyPatch]
public class NoiseSensorDetectionFix
{
    [HarmonyPostfix]
    [HarmonyPatch(typeof(NoiseMaker), nameof(NoiseMaker.GetNoiseOrigin))]
    public static void FixNoiseMakerOrigin(NoiseMaker __instance, ref Vector3 __result)
    {
        // Someone might need to change the origin for a custom feature, making sure this doesn't override that
        if (__result == __instance._attachedBody.GetCenterOfMass())
        {
            __result = __instance._attachedBody.GetWorldCenterOfMass();
        }
    }
}