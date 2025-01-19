using HarmonyLib;

namespace VanillaFix;

/// <summary>
/// UiSizeSetterDialogueOption changes the dialogue option's text's horizontalOverflow as Overflow when the language is CJK (Chinese, Japanese, or Koreans).
/// This causes an overflow problem when the dialogue option's text is so long.
/// See https://github.com/JohnCorby/ow-vanilla-fix/issues/26
/// </summary>
[HarmonyPatch]
public class DialogueOptionOverflowInCJKFix
{
    [HarmonyPostfix, HarmonyPatch(typeof(UiSizeSetterDialogueOption), nameof(UiSizeSetterDialogueOption.DoResizeAction))]
    public static void UiSizeSetterDialogueOption_DoResizeAction(UiSizeSetterDialogueOption __instance, UITextSize textSizeSetting)
    {
        if(TextTranslation.Get().IsLanguageCJK())
        {
            __instance._textField.horizontalOverflow = UnityEngine.HorizontalWrapMode.Wrap;
        }
    }
}
