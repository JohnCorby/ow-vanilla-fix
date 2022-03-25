using HarmonyLib;
using OWML.ModHelper;
using System.Reflection;

namespace VanillaFix
{
    public class Mod : ModBehaviour
    {
        public static Mod Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());
        }
    }
}