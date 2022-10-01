using HarmonyLib;

namespace VanillaFix;

/// <summary>
/// changing literally any setting would cause SetLanguage to be called, which should only be possible to change in title screen.
/// </summary>
[HarmonyPatch(typeof(TextTranslation))]
public static class SetLanguageFix
{
	[HarmonyPrefix]
	[HarmonyPatch(nameof(TextTranslation.SetLanguage))]
	public static bool SetLanguage(TextTranslation __instance, TextTranslation.Language lang) =>
		// don't do language things if we're not in the title screen since that's supposed to be impossible
		LoadManager.GetCurrentScene() == OWScene.TitleScreen;
}
