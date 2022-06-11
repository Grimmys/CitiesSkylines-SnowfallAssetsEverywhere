using HarmonyLib;

namespace SnowfallAssetsEverywhere
{
    public static class HarmonyPatcher
    {
        private const string HarmonyId = "grimmys.snowfallassetseverywhere";
        private static bool patched = false;

        public static void PatchAll()
        {
            if (patched) return;

            patched = true;
            var harmony = new Harmony(HarmonyId);
            harmony.PatchAll(typeof(HarmonyPatcher).Assembly);
        }

        public static void UnpatchAll()
        {
            if (!patched) return;

            var harmony = new Harmony(HarmonyId);
            harmony.UnpatchAll(HarmonyId);
            patched = false;
        }
    }
}
