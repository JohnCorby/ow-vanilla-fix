using HarmonyLib;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace VanillaFix;

/// <summary>
/// Item name does not update when looking between items
/// </summary>
[HarmonyPatch(typeof(ItemTool))]
public static class ItemPromptFix
{
    public static string _itemName;

    /// <summary>
    /// Allows the prompt to change if the item name is different
    /// </summary>
    [HarmonyTranspiler]
    [HarmonyPatch(nameof(ItemTool.UpdateState))]
    public static IEnumerable<CodeInstruction> ItemTool_UpdateState_Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        // 
        // Original logic:
        //     if (this._promptState != newState)
        //     {
        //         this._promptState = newState;
        //
        // Modified logic:
        //     if (this._promptState != newState || ItemPromptFix._itemName != itemName)
        //     {
        //         ItemPromptFix._itemName = itemName;
        //         this._promptState = newState;
        // 
        return new CodeMatcher(instructions, generator).MatchForward(true,
                new CodeMatch(OpCodes.Ldarg_0),
                new CodeMatch(OpCodes.Ldfld, AccessTools.Field(typeof(ItemTool), nameof(ItemTool._promptState))),
                new CodeMatch(OpCodes.Ldarg_1),
                new CodeMatch(OpCodes.Beq)) // this._promptState != newState
            .Advance(1).Insert(
                new CodeInstruction(OpCodes.Ldarg_2),
                new CodeInstruction(OpCodes.Stsfld, AccessTools.Field(typeof(ItemPromptFix), nameof(_itemName))) // _itemName = itemName;
            ).CreateLabel(out Label promptUpdate).Advance(-1).Insert(
                new CodeInstruction(OpCodes.Bne_Un, promptUpdate),
                new CodeInstruction(OpCodes.Ldsfld, AccessTools.Field(typeof(ItemPromptFix), nameof(_itemName))),
                new CodeInstruction(OpCodes.Ldarg_2) // _itemName != itemName;
            ).InstructionEnumeration();
    }
}
