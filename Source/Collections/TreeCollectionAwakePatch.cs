using HarmonyLib;

namespace SnowfallAssetsEverywhere
{
    [HarmonyPatch(typeof(TreeCollection), "Awake")]
    public static class TreeCollectionAwakePatch
    {
        public static bool Prefix(TreeCollection __instance)
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
