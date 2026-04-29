using HarmonyLib;
using UnityEngine;

namespace VanillaFix;

/// <summary>
/// CREDIT TO MEGAPIGGY FOR THIS FIX
/// 
/// ButtonPromptLibrary is missing a few button textures.
/// this adds them.
/// </summary>
[HarmonyPatch(typeof(ButtonPromptLibrary))]
public static class ButtonPromptFix
{
	[HarmonyPostfix]
	[HarmonyPatch(nameof(ButtonPromptLibrary.Initialize))]
	private static void Initialize()
	{
		ButtonPromptLibrary.s_keyCodeDict[KeyCode.Equals] = Mod.EqualsButton;
		ButtonPromptLibrary.s_keyCodeDict[KeyCode.KeypadEquals] = Mod.EqualsButton;
		ButtonPromptLibrary.s_keyCodeDict[KeyCode.Comma] = Mod.CommaButton;
		ButtonPromptLibrary.s_keyCodeDict[KeyCode.Period] = Mod.PeriodButton;
		ButtonPromptLibrary.s_keyCodeDict[KeyCode.KeypadPeriod] = Mod.PeriodButton;
		ButtonPromptLibrary.s_keyCodeDict[KeyCode.AltGr] = Mod.AltGrButton;
		ButtonPromptLibrary.s_keyCodeDict[KeyCode.BackQuote] = Mod.BackquoteButton;
		ButtonPromptLibrary.s_keyCodeDict[KeyCode.LeftApple] = Mod.AppleButton;
		ButtonPromptLibrary.s_keyCodeDict[KeyCode.RightApple] = Mod.AppleButton;

		ButtonPromptLibrary.s_axisTextureDict.Add(Mod.MouseDownScroll, AxisIdentifier.KEYBD_MOUSEWHEEL);
		ButtonPromptLibrary.s_axisTextureDict.Add(Mod.MouseUpScroll, AxisIdentifier.KEYBD_MOUSEWHEEL);
		ButtonPromptLibrary.s_axisTextureDict.Add(Mod.MouseYScroll, AxisIdentifier.KEYBD_MOUSEWHEEL);
	}

	[HarmonyPrefix]
	[HarmonyPatch(nameof(ButtonPromptLibrary.GetAxisTexture), typeof(AxisIdentifier), typeof(int))]
	private static bool GetAxisTexture_AxisDirection_Postfix(AxisIdentifier axisId, int axisDirection, ref Texture2D __result)
	{
		if (axisId == AxisIdentifier.KEYBD_MOUSEWHEEL)
		{
			if (axisDirection == -1)
			{
				__result = Mod.MouseDownScroll;
			}
			else if (axisDirection == 1)
			{
				__result = Mod.MouseUpScroll;
			}
			else
			{
				__result = Mod.MouseYScroll;
			}
			return false;
		}
		return true;
	}
}
