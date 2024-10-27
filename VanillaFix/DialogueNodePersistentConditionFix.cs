using HarmonyLib;

namespace VanillaFix;

/// <summary>
/// fixes https://github.com/JohnCorby/ow-vanilla-fix/issues/17
/// persistent conditions were basically ignored
/// </summary>
[HarmonyPatch]
public static class DialogueNodePersistentConditionFix
{
	[HarmonyPrefix, HarmonyPatch(typeof(DialogueNode), nameof(DialogueNode.EntryConditionsSatisfied))]
	private static bool DialogueNode_EntryConditionsSatisfied(DialogueNode __instance, out bool __result)
	{
		bool flag = true;
		if (__instance._listEntryCondition.Count == 0)
		{
			__result = false;
			return false;
		}
		DialogueConditionManager sharedInstance = DialogueConditionManager.SharedInstance;
		for (int i = 0; i < __instance._listEntryCondition.Count; i++)
		{
			string text = __instance._listEntryCondition[i];
			if (sharedInstance.ConditionExists(text))
			{
				if (!sharedInstance.GetConditionState(text))
				{
					flag = false;
				}
			}
			// CHANGED: remove the !
			else if (PlayerData.PersistentConditionExists(text))
			{
				if (!PlayerData.GetPersistentCondition(text))
				{
					flag = false;
				}
			}
			else
			{
				flag = false;
			}
		}
		__result = flag;
		return false;
	}
}
