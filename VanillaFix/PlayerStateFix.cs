using HarmonyLib;

namespace VanillaFix;

/// <summary>
/// PlayerState.Reset doesn't reset all the fields.
/// this makes it do that.
/// </summary>
[HarmonyPatch(typeof(PlayerState))]
public static class PlayerStateFix
{
	[HarmonyPrefix]
	[HarmonyPatch(nameof(PlayerState.Reset))]
	private static bool Reset()
	{
		PlayerState._hasPlayerEnteredShip = default;
		PlayerState._isWearingSuit = default;
		PlayerState._alignedWithField = default;
		PlayerState._insideShip = default;
		PlayerState._insideShuttle = default;
		PlayerState._atFlightConsole = default;
		PlayerState._isCameraUnderwater = default;
		PlayerState._isDead = default;
		PlayerState._isResurrected = default;
		PlayerState._isAttached = default;
		PlayerState._usingTelescope = default;
		PlayerState._usingShipComputer = default;
		PlayerState._inLandingView = default;
		PlayerState._mapView = default;
		PlayerState._inBrambleDimension = default;
		PlayerState._inGiantsDeep = default;
		PlayerState._isFlashlightOn = default;
		PlayerState._inDarkZone = default;
		PlayerState._inConversation = default;
		PlayerState._inZeroGTraining = default;
		PlayerState._isCameraLockedOn = default;
		PlayerState._isHullBreached = default;
		PlayerState._usingNomaiRemoteCamera = default;
		PlayerState._insideTheEye = default;
		PlayerState._isSleepingAtCampfire = default;
		PlayerState._isSleepingAtDreamCampfire = default;
		PlayerState._isFastForwarding = default;
		PlayerState._inCloakingField = default;
		PlayerState._inDreamWorld = default;
		PlayerState._hasPlayerhadLanternBlownOut = default;
		PlayerState._viewingProjector = default;
		PlayerState._isPeeping = default;
		PlayerState._isGrabbedByGhost = default;
		PlayerState._lastDetachTime = default;
		PlayerState._undertowVolumeCount = default;
		PlayerState._playerFogWarpDetector = default;
		PlayerState._quantumMoon = default;
		return false;
	}
}
