using HarmonyLib;
using OWML.ModHelper;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace VanillaFix;

public class Mod : ModBehaviour
{
	public static Texture2D EqualsButton { get; private set; }
	public static Texture2D AltGrButton { get; private set; }

	private void Awake() => Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());

	private void Start()
	{
		EqualsButton = CreateButtonTexture("Keyboard_Black_Equals");
		AltGrButton = CreateButtonTexture("Keyboard_Black_AltGr");
	}

	private Texture2D CreateButtonTexture(string name)
	{
		var texture = new Texture2D(2, 2, TextureFormat.RGBA32, false);
		texture.name = name;
		texture.wrapMode = TextureWrapMode.Clamp;
		texture.LoadImage(File.ReadAllBytes(Path.Combine(ModHelper.Manifest.ModFolderPath, name + ".png")));
		return texture;
	}
}
