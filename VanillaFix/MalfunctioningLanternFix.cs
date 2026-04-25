using HarmonyLib;
using UnityEngine;

namespace VanillaFix;

/// <summary>
/// Don't make conceal or focus sounds for invalid, nonfunctioning, and malfunctioning dream lanterns
/// </summary>
[HarmonyPatch(typeof(DreamLanternItem))]
public static class MalfunctioningLanternFix
{
	[HarmonyPrefix]
	[HarmonyPatch(nameof(DreamLanternItem.Update))]
	private static bool Update(DreamLanternItem __instance)
	{
		switch (__instance.GetLanternType())
		{
			// don't allow the bad dream lanterns to update conceal and focus
			case DreamLanternType.Invalid:
			case DreamLanternType.Nonfunctioning:
			case DreamLanternType.Malfunctioning:
				return false;
			case DreamLanternType.Functioning:
			default: // Just in case some other mod adds a new type
				return true;
		}
	}
}
