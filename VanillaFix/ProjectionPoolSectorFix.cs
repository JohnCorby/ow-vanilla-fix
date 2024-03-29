﻿using HarmonyLib;
using UnityEngine;

namespace VanillaFix;

/// <summary>
/// _visualSector2 doesn't do all the same checks _visualSector does.
/// this means that sector stuff breaks in rare cases.
///
/// they also accidentally check the current visual sector instead of the paired visual sector.
/// </summary>
[HarmonyPatch(typeof(NomaiRemoteCameraPlatform))]
public static class ProjectionPoolSectorFix
{
	// no im not doing a transpiler BLEHHHHHHHHH
	[HarmonyPrefix]
	[HarmonyPatch(typeof(NomaiRemoteCameraPlatform), nameof(NomaiRemoteCameraPlatform.SwitchToRemoteCamera))]
	private static bool SwitchToRemoteCamera(NomaiRemoteCameraPlatform __instance)
	{
		GlobalMessenger.FireEvent("EnterNomaiRemoteCamera");
		__instance._slavePlatform.RevealFactID();
		__instance._slavePlatform._ownedCamera.Activate(__instance, __instance._playerCamera);
		__instance._slavePlatform._ownedCamera.SetImageEffectFade(0f);
		__instance._alreadyOccupiedSectors.Clear();
		if (__instance._slavePlatform._visualSector != null)
		{
			if (__instance._slavePlatform._visualSector.ContainsOccupant(DynamicOccupant.Player))
			{
				__instance._alreadyOccupiedSectors.Add(__instance._slavePlatform._visualSector);
			}
			__instance._slavePlatform._visualSector.AddOccupant(Locator.GetPlayerSectorDetector());
			Sector sector = __instance._slavePlatform._visualSector.GetParentSector();
			while (sector != null)
			{
				if (sector.ContainsOccupant(DynamicOccupant.Player))
				{
					__instance._alreadyOccupiedSectors.Add(sector);
				}
				sector.AddOccupant(Locator.GetPlayerSectorDetector());
				sector = sector.GetParentSector();
			}
		}
		if (__instance._slavePlatform._visualSector2 != null)
		{
			if (__instance._slavePlatform._visualSector2.ContainsOccupant(DynamicOccupant.Player))
			{
				__instance._alreadyOccupiedSectors.Add(__instance._slavePlatform._visualSector2);
			}
			__instance._slavePlatform._visualSector2.AddOccupant(Locator.GetPlayerSectorDetector());
			Sector sector = __instance._slavePlatform._visualSector2.GetParentSector();
			while (sector != null)
			{
				if (sector.ContainsOccupant(DynamicOccupant.Player))
				{
					__instance._alreadyOccupiedSectors.Add(sector);
				}
				sector.AddOccupant(Locator.GetPlayerSectorDetector());
				sector = sector.GetParentSector();
			}
		}
		if (__instance._slavePlatform._darkZone != null)
		{
			__instance._slavePlatform._darkZone.AddPlayerToZone(true);
		}

		return false;
	}

	[HarmonyPrefix]
	[HarmonyPatch(nameof(NomaiRemoteCameraPlatform.SwitchToPlayerCamera))]
	private static bool SwitchToPlayerCamera(NomaiRemoteCameraPlatform __instance)
	{
		if (__instance._slavePlatform._visualSector != null)
		{
			if (!__instance._alreadyOccupiedSectors.Contains(__instance._slavePlatform._visualSector))
			{
				__instance._slavePlatform._visualSector.RemoveOccupant(Locator.GetPlayerSectorDetector());
			}
			Sector sector = __instance._slavePlatform._visualSector.GetParentSector();
			while (sector != null)
			{
				if (!__instance._alreadyOccupiedSectors.Contains(sector))
				{
					sector.RemoveOccupant(Locator.GetPlayerSectorDetector());
				}
				sector = sector.GetParentSector();
			}
		}
		if (__instance._slavePlatform._visualSector2 != null)
		{
			if (!__instance._alreadyOccupiedSectors.Contains(__instance._slavePlatform._visualSector2))
			{
				__instance._slavePlatform._visualSector2.RemoveOccupant(Locator.GetPlayerSectorDetector());
			}
			Sector sector = __instance._slavePlatform._visualSector2.GetParentSector();
			while (sector != null)
			{
				if (!__instance._alreadyOccupiedSectors.Contains(sector))
				{
					sector.RemoveOccupant(Locator.GetPlayerSectorDetector());
				}
				sector = sector.GetParentSector();
			}
		}
		if (__instance._slavePlatform._darkZone != null)
		{
			__instance._slavePlatform._darkZone.RemovePlayerFromZone(true);
		}
		GlobalMessenger.FireEvent("ExitNomaiRemoteCamera");
		__instance._slavePlatform._ownedCamera.Deactivate();
		__instance._slavePlatform._ownedCamera.SetImageEffectFade(0f);

		return false;
	}

	[HarmonyPrefix]
	[HarmonyPatch(nameof(NomaiRemoteCameraPlatform.VerifySectorOccupancy))]
	private static bool VerifySectorOccupancy(NomaiRemoteCameraPlatform __instance)
	{
		if (__instance._slavePlatform._visualSector != null && !__instance._slavePlatform._visualSector.ContainsOccupant(DynamicOccupant.Player))
		{
			Debug.LogWarning("Player was somehow removed from the NomaiRemoteCameraPlatform's visual sectors!  Re-adding...");
			__instance._slavePlatform._visualSector.AddOccupant(Locator.GetPlayerSectorDetector());
			Sector sector = __instance._slavePlatform._visualSector.GetParentSector();
			while (sector != null)
			{
				if (!sector.ContainsOccupant(DynamicOccupant.Player))
				{
					sector.AddOccupant(Locator.GetPlayerSectorDetector());
				}
				sector = sector.GetParentSector();
			}
		}
		if (__instance._slavePlatform._visualSector2 != null && !__instance._slavePlatform._visualSector2.ContainsOccupant(DynamicOccupant.Player))
		{
			Debug.LogWarning("Player was somehow removed from the NomaiRemoteCameraPlatform's visual sectors!  Re-adding...");
			__instance._slavePlatform._visualSector2.AddOccupant(Locator.GetPlayerSectorDetector());
			Sector sector = __instance._slavePlatform._visualSector2.GetParentSector();
			while (sector != null)
			{
				if (!sector.ContainsOccupant(DynamicOccupant.Player))
				{
					sector.AddOccupant(Locator.GetPlayerSectorDetector());
				}
				sector = sector.GetParentSector();
			}
		}

		return false;
	}
}
