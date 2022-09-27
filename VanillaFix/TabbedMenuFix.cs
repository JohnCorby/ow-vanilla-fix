using HarmonyLib;
using UnityEngine.UI;

namespace VanillaFix;

/// <summary>
/// TabbedMenu.Deactivate NRE's when Locator.GetEventSystem().currentSelectedGameObject is null (which it is for the owml mod sub menu)
/// </summary>
[HarmonyPatch]
public class TabbedMenuFix
{
	[HarmonyReversePatch]
	[HarmonyPatch(typeof(Menu), nameof(Menu.Deactivate))]
	private static void base_Deactivate(Menu __instance, bool remainVisible = false) { }

	[HarmonyPrefix]
	[HarmonyPatch(typeof(TabbedMenu), nameof(TabbedMenu.Deactivate))]
	private static bool TabbedMenu_Deactivate(TabbedMenu __instance, bool keepPreviousMenuVisible = false)
	{
		if (Locator.GetEventSystem().currentSelectedGameObject)
			__instance._lastSelectableOnDeactivate = Locator.GetEventSystem().currentSelectedGameObject.GetComponent<Selectable>();
		foreach (var tabSelectablePair in __instance._tabSelectablePairs)
			tabSelectablePair.tabButton.Enable(false);
		base_Deactivate(__instance, keepPreviousMenuVisible);
		Locator.GetMenuInputModule().OnInputModuleTab -= __instance.OnInputModuleTabEvent;
		return false;
	}
}
