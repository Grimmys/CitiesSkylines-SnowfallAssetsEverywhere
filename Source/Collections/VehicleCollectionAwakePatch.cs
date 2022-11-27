using System.Linq;
using HarmonyLib;

namespace SnowfallAssetsEverywhere
{
    [HarmonyPatch(typeof(VehicleCollection), "Awake")]
    public static class VehicleCollectionAwakePatch
    {
        public static bool Prefix(VehicleCollection __instance)
        {
            if (Utils.ShouldBeSkipped(__instance))
            {
                if (__instance.gameObject?.name != Constants.WINTER_GARBAGE)
                {
                    UnityEngine.Object.Destroy(__instance);
                    return false;
                }
                __instance.m_prefabs = __instance.m_prefabs.Where(prefab => prefab.name == "Snowplow").ToArray();
            }
            return true;
        }
    }
}
