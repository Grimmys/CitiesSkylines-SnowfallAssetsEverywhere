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
                if (__instance.gameObject?.name != Constants.WINTER_BEAUTIFICATION && __instance.gameObject?.name != Constants.WINTER_MONUMENT)
                {
                    UnityEngine.Object.Destroy(__instance);
                    return false;
                }
            }
            return true;
        }
    }
}