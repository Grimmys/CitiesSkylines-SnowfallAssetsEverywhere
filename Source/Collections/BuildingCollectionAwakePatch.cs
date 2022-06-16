using System.Linq;
using HarmonyLib;

namespace SnowfallAssetsEverywhere
{

    [HarmonyPatch(typeof(BuildingCollection), "Awake")]
    public static class BuildingCollectionAwakePatch
    {
        public struct Prefabs
        {
            public BuildingInfo[] prefabs;
            public string[] replacedNames;

            public Prefabs(BuildingInfo[] prefabs, string[] replacedNames)
            {
                this.prefabs = prefabs;
                this.replacedNames = replacedNames;
            }
        }

        public static bool Prefix(BuildingCollection __instance, out Prefabs __state)
        {
            __state = new Prefabs((BuildingInfo[])__instance.m_prefabs.Clone(), (string[])__instance.m_replacedNames.Clone());
            if (Utils.ShouldBeSkipped(__instance))
            {
                if (__instance.gameObject?.name != Constants.WINTER_BEAUTIFICATION && __instance.gameObject?.name != Constants.WINTER_MONUMENT 
                    && __instance.gameObject?.name != Constants.WINTER_GARBAGE && __instance.gameObject?.name != Constants.WINTER_INDUSTRIAL_FARMING && __instance.gameObject?.name != Constants.WINTER_EXPANSION_1)
                {
                    UnityEngine.Object.Destroy(__instance);
                    return false;
                }
                if (__instance.gameObject?.name == Constants.WINTER_GARBAGE)
                {
                    __instance.m_prefabs = __instance.m_prefabs.Where(prefab => prefab.name == "Snowdump").ToArray();
                    __instance.m_replacedNames = null;
                }
                if (__instance.gameObject?.name == Constants.WINTER_INDUSTRIAL_FARMING)
                {
                    __instance.m_prefabs = __instance.m_prefabs.Where(prefab => prefab.name.EndsWith("_Greenhouse")).ToArray();
                    __instance.m_replacedNames = null;
                }
                if (__instance.gameObject?.name == Constants.WINTER_EXPANSION_1)
                {
                    __instance.m_prefabs = __instance.m_prefabs.Where(prefab => prefab.name == "2x2_winter_fishing_pier"
                                      || prefab.name == "Snowmobile Track" ||
                                      prefab.name == "Ice_Fishing_Pond" || prefab.name == "Ice Hockey Rink").ToArray();
                    __instance.m_replacedNames = null;
                }
            }
            return true;
        }

        public static void Postfix(BuildingCollection __instance, Prefabs __state)
        {
                if (__instance.gameObject?.name == Constants.WINTER_INDUSTRIAL_FARMING)
                {
                    __instance.m_prefabs = __state.prefabs;
                    __instance.m_replacedNames = __state.replacedNames;
                }
        }
    }
}