using System.Linq;
using HarmonyLib;

namespace SnowfallAssetsEverywhere
{
    [HarmonyPatch(typeof(BuildingCollection), "Awake")]
    public static class BuildingCollectionAwakePatch
    {

        public static bool Prefix(BuildingCollection __instance, BuildingInfo[] ___m_prefabs, string[] ___m_replacedNames)
        {
            if (Utils.ShouldBeSkipped(__instance))
            {
                if (__instance.gameObject?.name != Constants.WINTER_BEAUTIFICATION && __instance.gameObject?.name != Constants.WINTER_MONUMENT && __instance.gameObject?.name != Constants.WINTER_GARBAGE)
                {
                    UnityEngine.Object.Destroy(__instance);
                    return false;
                }
                if (__instance.gameObject?.name == Constants.WINTER_GARBAGE)
                {
                    __instance.m_prefabs = __instance.m_prefabs.Where(prefab => prefab.name == "Snowdump").ToArray();
                    __instance.m_replacedNames = null;
                }
            }
            return true;
        }
    }
}