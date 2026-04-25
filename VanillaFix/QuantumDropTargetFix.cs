using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace VanillaFix;

/// <summary>
/// Items left on quantum objects do not travel with them
/// </summary>
[HarmonyPatch(typeof(VisibilityObject))]  // VisibilityObject because QuantumObject doesn't have its own Awake
public static class QuantumDropTargetFix
{
    [HarmonyPostfix]
    [HarmonyPatch(nameof(VisibilityObject.Awake))]
    public static void VisibilityObject_Awake_Postfix(VisibilityObject __instance)
    {
        if (__instance is QuantumObject)
        {
            __instance.gameObject.GetAddComponent<QuantumDropTarget>(); // Add only if there isn't one already
        }
    }
}

/// <summary>
/// Item drop target for quantum objects
/// </summary>
public class QuantumDropTarget : MonoBehaviour, IItemDropTarget
{
    public QuantumObject quantumObject;
    public QuantumState[] states;
    public List<OWItem> droppedItems;
    public QuantumState state;

    public QuantumState[] GetStates()
    {
        List<QuantumState> stateList = new List<QuantumState>();
        foreach (Transform child in transform)
        {
            if (child.TryGetComponent(out QuantumState state))
                stateList.Add(state);
        }
        return stateList.ToArray();
    }

    public QuantumState GetCurrentState()
    {
        if (states == null || states.Length == 0) return null;
        return states.FirstOrDefault(state => state.gameObject.activeSelf);
    }

    public void Awake()
    {
        droppedItems = new List<OWItem>();
        quantumObject = GetComponent<QuantumObject>();
        states = GetStates();
        state = GetCurrentState();
        quantumObject.OnPostCollapse += OnPostCollapse;
    }

    public void OnDestroy()
    {
        quantumObject.OnPostCollapse -= OnPostCollapse;
    }

    public void OnPostCollapse(QuantumObject quantumObject, bool collapsed)
    {
        state = GetCurrentState();

        if (state == null) return;
        if (droppedItems == null || droppedItems.Count == 0) return;

        foreach (var droppedItem in droppedItems)
        {
            if (droppedItem != null)
                droppedItem.transform.SetParent(state.transform);
        }
    }

    public void AddDroppedItem(GameObject dropTarget, OWItem item)
    {
        if (droppedItems != null) droppedItems.SafeAdd(item);
        item.onPickedUp += OnPickedUpDroppedItem;
    }

    private void OnPickedUpDroppedItem(OWItem item)
    {
        if (droppedItems != null) droppedItems.Remove(item);
        item.onPickedUp -= OnPickedUpDroppedItem;
    }

    public Transform GetItemDropTargetTransform(GameObject raycastTarget)
    {
        if (state != null) return state.transform;
        return transform;
    }
}