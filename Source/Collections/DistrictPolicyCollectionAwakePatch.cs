using HarmonyLib;

namespace SnowfallAssetsEverywhere
{
    [HarmonyPatch(typeof(DistrictPolicyCollection), "Awake")]
    public static class DistrictPolicyCollectionAwakePatch
    {
        public static bool Prefix(DistrictPolicyCollection __instance)
        {
            return !Utils.IsNonNativeComponent(__instance);
        }
    }
}
