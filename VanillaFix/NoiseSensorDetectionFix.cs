using HarmonyLib;
using UnityEngine;

namespace VanillaFix;

/// <summary>
/// origin guy here uses local center of mass instead of world. this usually returns 0 or near-0.
/// doesnt break in base game because player world position is at 0 (because universe recenter blah blah) but this isnt the case for other things obviously.
/// </summary>
[HarmonyPatch]
public class NoiseSensorDetectionFix
{
    [HarmonyPostfix]
    [HarmonyPatch(typeof(NoiseMaker), nameof(NoiseMaker.GetNoiseOrigin))]
    public static void FixNoiseMakerOrigin(NoiseMaker __instance, ref Vector3 __result)
    {
        // Someone might need to change the origin for a custom feature, making sure this doesn't override that. That's why we're in Postfix
        if (__result == __instance._attachedBody.GetCenterOfMass())
        {
            __result = __instance._attachedBody.GetWorldCenterOfMass();
        }
    }
}
