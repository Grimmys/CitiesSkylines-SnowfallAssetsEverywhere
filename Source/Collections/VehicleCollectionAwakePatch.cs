using System.Linq;
using HarmonyLib;

namespace SnowfallAssetsEverywhere
{
    [HarmonyPatch(typeof(VehicleCollection), "Awake")]
    public static class VehicleCollectionAwakePatch
    {

        private static bool expansion7AlreadyLoaded = false;

        public static void ResetState()
        {
            expansion7AlreadyLoaded = false;
        }

        public static bool Prefix(VehicleCollection __instance)
        {
            if (Utils.IsNonNativeComponent(__instance))
            {
                if (__instance.gameObject?.name != Constants.WINTER_GARBAGE)
                {
                    UnityEngine.Object.Destroy(__instance);
                    return false;
                }
                __instance.m_prefabs = __instance.m_prefabs.Where(prefab => prefab.name == "Snowplow").ToArray();
            }
            else if (__instance.gameObject?.name == Constants.EXPANSION_7)
            {
                if (expansion7AlreadyLoaded)
                {
                    return false;
                }
                else
                {
                    expansion7AlreadyLoaded = true;
                }
            }
            return true;
        }
    }
}
