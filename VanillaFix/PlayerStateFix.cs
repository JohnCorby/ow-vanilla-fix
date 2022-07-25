using HarmonyLib;

namespace VanillaFix;

/// <summary>
/// PlayerState.Reset doesn't reset all the fields.
/// this makes it do that.
/// </summary>
[HarmonyPatch(typeof(PlayerState))]
public static class PlayerStateFix
{
	[HarmonyPostfix]
	[HarmonyPatch(nameof(PlayerState.Reset))]
	private static void Reset()
	{
		PlayerState._hasPlayerEnteredShip = false;
		PlayerState._isWearingSuit = false;
		PlayerState._alignedWithField = false;
		PlayerState._insideShip = false;
		PlayerState._insideShuttle = false;
		PlayerState._atFlightConsole = false;
		PlayerState._isCameraUnderwater = false;
		PlayerState._isDead = false;
		PlayerState._isResurrected = false;
		PlayerState._isAttached = false;
		PlayerState._usingTelescope = false;
		PlayerState._usingShipComputer = false;
		PlayerState._inLandingView = false;
		PlayerState._mapView = false;
		PlayerState._inBrambleDimension = false;
		PlayerState._inGiantsDeep = false;
		PlayerState._isFlashlightOn = false;
		PlayerState._inDarkZone = false;
		PlayerState._inConversation = false;
		PlayerState._inZeroGTraining = false;
		PlayerState._isCameraLockedOn = false;
		PlayerState._isHullBreached = false;
		PlayerState._usingNomaiRemoteCamera = false;
		PlayerState._insideTheEye = false;
		PlayerState._isSleepingAtCampfire = false;
		PlayerState._isSleepingAtDreamCampfire = false;
		PlayerState._isFastForwarding = false;
		PlayerState._inCloakingField = false;
		PlayerState._inDreamWorld = false;
		PlayerState._hasPlayerhadLanternBlownOut = false;
		PlayerState._viewingProjector = false;
		PlayerState._isPeeping = false;
		PlayerState._isGrabbedByGhost = false;
		PlayerState._lastDetachTime = 0;
		PlayerState._undertowVolumeCount = 0;
		PlayerState._playerFogWarpDetector = null;
		PlayerState._quantumMoon = null;
	}
}
