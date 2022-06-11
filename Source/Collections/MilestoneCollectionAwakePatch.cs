using HarmonyLib;

namespace SnowfallAssetsEverywhere
{
    [HarmonyPatch(typeof(MilestoneCollection), "Awake")]
    public static class MilestoneCollectionAwakePatch
    {
        public static bool Prefix(MilestoneCollection __instance)
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