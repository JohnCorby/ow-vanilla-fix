using HarmonyLib;
using OWML.ModHelper;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace VanillaFix;

public class Mod : ModBehaviour
{
	public static Texture2D EqualsButton { get; private set; }
	public static Texture2D CommaButton { get; private set; }
	public static Texture2D PeriodButton { get; private set; }
	public static Texture2D AltGrButton { get; private set; }
	public static Texture2D BackquoteButton { get; private set; }
	public static Texture2D AppleButton { get; private set; }

	private void Awake() => Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());

	private void Start()
	{
		EqualsButton = CreateButtonTexture("Keyboard_Black_Equals");
		CommaButton = CreateButtonTexture("Keyboard_Black_Comma");
		PeriodButton = CreateButtonTexture("Keyboard_Black_Period");
		AltGrButton = CreateButtonTexture("Keyboard_Black_AltGr");
		BackquoteButton = CreateButtonTexture("Keyboard_Black_Backquote");
		AppleButton = CreateButtonTexture("Keyboard_Black_Apple");
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
