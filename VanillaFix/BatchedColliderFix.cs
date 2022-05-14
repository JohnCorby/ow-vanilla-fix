using HarmonyLib;

namespace VanillaFix;

/// <summary>
/// this makes batched colliders stay loaded all the time
/// (i.e. they are considered required colliders)
/// so the ship doesn't fall through things
/// </summary>
[HarmonyPatch(typeof(StreamingGroup))]
public static class BatchedColliderFix
{
    [HarmonyPrefix]
    [HarmonyPatch(nameof(StreamingGroup.LoadRequiredColliders))]
    private static bool LoadRequiredColliders(StreamingGroup __instance, int priorityBias)
    {
        StreamingManager.LoadStreamingAssets(__instance._terrainColliderSceneMeshBundle, 18 + priorityBias);
        StreamingManager.LoadStreamingAssets(__instance._structuresColliderSceneMeshBundle, 17 + priorityBias);
        StreamingManager.LoadStreamingAssets(__instance._batchedRenderersColliderBundle, 18 + priorityBias);

        return false;
    }

    [HarmonyPrefix]
    [HarmonyPatch(nameof(StreamingGroup.UnloadRequiredColliders))]
    private static bool UnloadRequiredColliders(StreamingGroup __instance, float delay)
    {
        StreamingManager.UnloadStreamingAssets(__instance._terrainColliderSceneMeshBundle, delay);
        StreamingManager.UnloadStreamingAssets(__instance._structuresColliderSceneMeshBundle, delay);
        StreamingManager.UnloadStreamingAssets(__instance._batchedRenderersColliderBundle, delay);

        return false;
    }

    [HarmonyPrefix]
    [HarmonyPatch(nameof(StreamingGroup.LoadRequiredAssets))]
    private static bool LoadRequiredAssets(StreamingGroup __instance, int priorityBias)
    {
        if (__instance._locked) return false;

        StreamingManager.LoadStreamingAssets(__instance._bakedTerrainsBundle, 8 + priorityBias);
        StreamingManager.LoadStreamingAssets(__instance._batchedRenderersBundle, 8 + priorityBias);
        StreamingManager.LoadStreamingAssets(__instance._bakedVISRenderersBundle, 8 + priorityBias);
        StreamingManager.LoadStreamingAssets(__instance._terrainSceneMeshBundle, 8 + priorityBias);
        StreamingManager.LoadStreamingAssets(__instance._structuresSceneMeshBundle, 7 + priorityBias);
        if (!__instance._streamCollidersOnWakeUp)
        {
            StreamingManager.LoadStreamingAssets(__instance._terrainColliderSceneMeshBundle, 18 + priorityBias);
            StreamingManager.LoadStreamingAssets(__instance._structuresColliderSceneMeshBundle, 17 + priorityBias);
            StreamingManager.LoadStreamingAssets(__instance._batchedRenderersColliderBundle, 18 + priorityBias);
        }

        __instance._requiredAssetsPriorityBias = priorityBias;

        return false;
    }

    [HarmonyPrefix]
    [HarmonyPatch(nameof(StreamingGroup.UnloadRequiredAssets))]
    private static bool UnloadRequiredAssets(StreamingGroup __instance, float delay)
    {
        if (__instance._locked) return false;

        StreamingManager.UnloadStreamingAssets(__instance._bakedTerrainsBundle, delay);
        StreamingManager.UnloadStreamingAssets(__instance._batchedRenderersBundle, delay);
        StreamingManager.UnloadStreamingAssets(__instance._bakedVISRenderersBundle, delay);
        StreamingManager.UnloadStreamingAssets(__instance._terrainSceneMeshBundle, delay);
        StreamingManager.UnloadStreamingAssets(__instance._structuresSceneMeshBundle, delay);

        return false;
    }
}