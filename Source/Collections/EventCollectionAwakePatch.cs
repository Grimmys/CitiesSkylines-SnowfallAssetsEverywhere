using HarmonyLib;

namespace SnowfallAssetsEverywhere
{
    [HarmonyPatch(typeof(EventCollection), "Awake")]
    public static class EventCollectionAwakePatch
    {
        public static bool Prefix(EventCollection __instance)
        {
            if (Utils.IsNonNativeComponent(__instance))
            {
                UnityEngine.Object.Destroy(__instance);
                return false;
            }
            return true;
        }
    }
}
