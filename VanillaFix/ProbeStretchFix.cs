using HarmonyLib;
using System;
using System.Reflection;
using UnityEngine;

namespace VanillaFix;

/// <summary>
/// fixes https://github.com/JohnCorby/ow-vanilla-fix/issues/7
///
/// not perfect because of lossy scale, but this is literally the best unity can do
/// </summary>
[HarmonyPatch(typeof(ProbeAnchor))]
public static class ProbeStretchFix
{
	[HarmonyPrefix]
	[HarmonyPatch(nameof(ProbeAnchor.AnchorToObject))]
	private static bool AnchorToObject(ProbeAnchor __instance, GameObject hitObject, Vector3 hitNormal, Vector3 hitPoint)
	{
		OWRigidbody attachedOWRigidbody = hitObject.GetAttachedOWRigidbody(false);
		if (attachedOWRigidbody.GetMass() < 0.001f)
		{
			return false;
		}
		if (attachedOWRigidbody.GetMass() < 100f)
		{
			Vector3 vector = __instance._probeBody.GetVelocity() - attachedOWRigidbody.GetPointVelocity(__instance._probeBody.GetPosition());
			attachedOWRigidbody.GetRigidbody().AddForceAtPosition(vector.normalized * 0.005f, hitPoint, ForceMode.Impulse);
		}
		__instance._collider.enabled = false;
		__instance._breakableFragment = hitObject.GetComponentInParent<FragmentIntegrity>();
		__instance._sandLevelController = null;
		AstroObject component = attachedOWRigidbody.GetComponent<AstroObject>();
		if (component != null)
		{
			__instance._sandLevelController = component.GetSandLevelController();
		}
		__instance._ringworldDam = hitObject.GetComponentInParent<DamDestructionController>();
		if (__instance._breakableFragment != null || __instance._ringworldDam != null)
		{
			__instance._probeNotification.displayMessage = __instance.BuildIntegrityString();
			if (__instance._notificationPosted)
			{
				NotificationManager.SharedInstance.RepostNotifcation(__instance._probeNotification);
			}
			else
			{
				NotificationManager.SharedInstance.PostNotification(__instance._probeNotification, true);
				__instance._notificationPosted = true;
			}
		}
		__instance._probeBody.MakeKinematic();
		__instance._probeBody.transform.rotation = Quaternion.FromToRotation(__instance._probeBody.transform.forward, -hitNormal) * __instance._probeBody.transform.rotation;
		__instance._probeBody.transform.position = hitPoint;
		// parent AFTER rotating instead of before, so it adjusts scale using the correct orientation instead of what it was launch at
		__instance._probeBody.transform.parent = hitObject.transform;
		__instance._probeBody.GetAttachedForceDetector().enabled = false;
		__instance._probeBody.GetAttachedFluidDetector().enabled = false;
		__instance._probeAlignment.enabled = false;
		__instance._localImpactPos = __instance.transform.localPosition;
		__instance._anchorTime = Time.time;
		__instance._anchored = true;
		// i should really use a transpiler instead of doing this but i dont want to
		__instance.RaiseEvent(nameof(__instance.OnAnchorToSurface));

		return false;
	}

	// copied from QSB
	private static void RaiseEvent<T>(this T instance, string eventName, params object[] args)
	{
		const BindingFlags flags = BindingFlags.Instance
			| BindingFlags.Static
			| BindingFlags.Public
			| BindingFlags.NonPublic
			| BindingFlags.DeclaredOnly;
		if (typeof(T)
				.GetField(eventName, flags)?
				.GetValue(instance) is not MulticastDelegate multiDelegate)
		{
			return;
		}

		multiDelegate.DynamicInvoke(args);
	}
}
