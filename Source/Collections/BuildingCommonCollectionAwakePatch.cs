using HarmonyLib;

namespace SnowfallAssetsEverywhere
{
    [HarmonyPatch(typeof(BuildingCommonCollection), "Awake")]
    public static class BuildingCommonCollectionAwakePatch
    {
        public static bool Prefix(BuildingCommonCollection __instance)
        {
            return !Utils.ShouldBeSkipped(__instance);
        }
    }
}