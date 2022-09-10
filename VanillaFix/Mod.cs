using HarmonyLib;
using OWML.ModHelper;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace VanillaFix;

public class Mod : ModBehaviour
{
	public static Texture2D EqualsButton { get; private set; }

	private void Awake() => Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());

	private void Start()
	{
		var texture = new Texture2D(2, 2, TextureFormat.RGBA32, false);
		texture.name = "Keyboard_Black_Equals";
		texture.wrapMode = TextureWrapMode.Clamp;
		texture.LoadImage(File.ReadAllBytes(Path.Combine(ModHelper.Manifest.ModFolderPath, "Keyboard_Black_Equals.png")));
		EqualsButton = texture;
	}
}
