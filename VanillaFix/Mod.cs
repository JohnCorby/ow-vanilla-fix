using HarmonyLib;
using OWML.ModHelper;
using System.Reflection;

namespace VanillaFix;

public class Mod : ModBehaviour
{
	private void Awake() => Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());
}
