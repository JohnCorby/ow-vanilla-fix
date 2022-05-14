using HarmonyLib;
using UnityEngine;

namespace VanillaFix;

/// <summary>
/// CREDIT TO NEBULA FOR THIS FIX
/// <para/>
/// makes it so that the marker position changing from center-arrow to side-arrow
/// happens at the edge of the screen for smaller resolutions.
/// in vanilla, that becomes more wrong the smaller the screen is.
/// </summary>
[HarmonyPatch(typeof(CanvasMarker))]
public static class CanvasMarkerFix
{
    [HarmonyPrefix]
    [HarmonyPatch(nameof(CanvasMarker.IsOnScreen),
        new[] { typeof(Vector3), typeof(Vector2) },
        new[] { ArgumentType.Normal, ArgumentType.Ref })]
    private static bool IsOnScreen(CanvasMarker __instance, out bool __result,
        Vector3 targetWorldPos, ref Vector2 onScreenPos)
    {
        onScreenPos.x = 0f;
        onScreenPos.y = 0f;

        __result = false;

        if (__instance._prevMarker == null)
        {
            var camera = __instance._canvas.worldCamera;
            if (camera == null)
                camera = Locator.GetActiveCamera().mainCamera;

            var canvasPos = __instance._canvas.WorldToCanvasPosition(camera, targetWorldPos);

            var width = __instance._canvas.pixelRect.width * (1080 / __instance._canvas.pixelRect.height);
            var height = 1080 - __instance.GetTotalMarkerHeight();

            var screenSize = __instance.GetMarkerTargetScreenSize() * 0.5f;
            screenSize = Mathf.Clamp(screenSize, 0f, height - canvasPos.y);

            canvasPos.y += screenSize;

            onScreenPos.x = canvasPos.x;
            onScreenPos.y = canvasPos.y;

            if (canvasPos.x >= 0f &&
                canvasPos.x <= width &&
                canvasPos.y >= 0f &&
                canvasPos.y <= height &&
                canvasPos.z > 0f)
                __result = true;
        }
        else
        {
            __result = __instance._prevMarker.IsOnScreen() && __instance.IsVisible();
        }

        return false;
    }
}