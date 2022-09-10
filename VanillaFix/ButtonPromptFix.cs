using HarmonyLib;
using UnityEngine;

namespace VanillaFix;

/// <summary>
/// ButtonPromptLibrary is missing an equals button texture.
/// this adds one.
/// </summary>
[HarmonyPatch(typeof(ButtonPromptLibrary))]
public static class ButtonPromptFix
{ 
    [HarmonyPostfix]
    [HarmonyPatch(nameof(ButtonPromptLibrary.Initialize))]
    public static void Initialize()
    {
        ButtonPromptLibrary.s_keyCodeDict[KeyCode.Equals] = Mod.EqualsButton;
        ButtonPromptLibrary.s_keyCodeDict[KeyCode.KeypadEquals] = Mod.EqualsButton;
    }
}