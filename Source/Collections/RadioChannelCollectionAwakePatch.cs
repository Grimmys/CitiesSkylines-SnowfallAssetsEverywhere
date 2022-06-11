using HarmonyLib;

namespace SnowfallAssetsEverywhere
{
    [HarmonyPatch(typeof(RadioChannelCollection), "Awake")]
    public static class RadioChannelCollectionAwakePatch
    {
        public static bool Prefix(RadioChannelCollection __instance)
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