using HarmonyLib;

namespace SnowfallAssetsEverywhere
{
    [HarmonyPatch(typeof(CitizenCollection), "Awake")]
    public static class CitizenCollectionAwakePatch
    {
        public static bool Prefix(CitizenCollection __instance)
        {
            if (Utils.ShouldBeSkipped(__instance))
            {
                UnityEngine.Object.Destroy(__instance);
                return false;
            }
            return true;
        }
    }
}