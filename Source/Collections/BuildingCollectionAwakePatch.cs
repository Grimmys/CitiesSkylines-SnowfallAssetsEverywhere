using System.Linq;
using System.Text.RegularExpressions;
using HarmonyLib;

namespace SnowfallAssetsEverywhere
{

    [HarmonyPatch(typeof(BuildingCollection), "Awake")]
    public static class BuildingCollectionAwakePatch
    {
        public struct OriginalPrefabs
        {
            public BuildingInfo[] prefabs;
            public string[] replacedNames;

            public OriginalPrefabs(BuildingInfo[] prefabs, string[] replacedNames)
            {
                this.prefabs = prefabs;
                this.replacedNames = replacedNames;
            }
        }

        private static readonly string[] relevantWinterCollections = { Constants.WINTER_BEAUTIFICATION, Constants.WINTER_MONUMENT, Constants.WINTER_GARBAGE, Constants.WINTER_INDUSTRIAL_FARMING, Constants.WINTER_EXPANSION_1 };
        private static readonly string[] impactedByChangeCollections = relevantWinterCollections.Concat(new []{Constants.INDUSTRIAL_FARMING, Constants.EXPANSION_1, Constants.WINTER_SIGNUP_PACK}).ToArray();

        private static readonly string[] garbageWinterBuildings = { "Snowdump" };
        private static readonly string[] industrialFarmingWinterBuildings = { "4x4_Greenhouse", "3x2_Greenhouse", "2x2_Greenhouse" };
        private static readonly string[] afterDarkExpansionWinterBuildings = { "2x2_winter_fishing_pier", "Snowmobile Track", "Ice_Fishing_Pond", "Ice Hockey Rink" };

        private static void RemoveReplacesFor(string[] replacedNames, string pattern)
        {
            var regex = new Regex(pattern);
            for (int i = 0; i < replacedNames.Length; i++)
            {
                if (regex.IsMatch(replacedNames[i]))
                {
                    replacedNames[i] = null;
                }
            }
        }
        public static bool Prefix(BuildingCollection __instance, out OriginalPrefabs __state)
        {
            __state = new OriginalPrefabs((BuildingInfo[])__instance.m_prefabs.Clone(), (string[])__instance.m_replacedNames.Clone());
            if (Utils.ShouldBeSkipped(__instance))
            {
                if (!relevantWinterCollections.Contains(__instance.gameObject?.name))
                {
                    UnityEngine.Object.Destroy(__instance);
                    return false;
                }
                __instance.m_replacedNames = null;
                if (__instance.gameObject?.name == Constants.WINTER_GARBAGE)
                {
                    __instance.m_prefabs = __instance.m_prefabs.Where(prefab => garbageWinterBuildings.Contains(prefab.name)).ToArray();
                }
                else if (__instance.gameObject?.name == Constants.WINTER_INDUSTRIAL_FARMING)
                {
                    __instance.m_prefabs = __instance.m_prefabs.Where(prefab => industrialFarmingWinterBuildings.Contains(prefab.name)).ToArray();
                }
                else if (__instance.gameObject?.name == Constants.WINTER_EXPANSION_1)
                {
                    __instance.m_prefabs = __instance.m_prefabs.Where(prefab => afterDarkExpansionWinterBuildings.Contains(prefab.name)).ToArray();
                }
            }
            if (__instance.gameObject?.name == Constants.INDUSTRIAL_FARMING)
            {
                RemoveReplacesFor(__instance.m_replacedNames, "_Greenhouse$");
            }
            if (__instance.gameObject?.name == Constants.EXPANSION_1)
            {
                RemoveReplacesFor(__instance.m_replacedNames, "^2x2_winter_fishing_pier$");
            }
            if (__instance.gameObject?.name == Constants.WINTER_SIGNUP_PACK)
            {
                __instance.m_replacedNames = null;
            }

            return true;
        }

        public static void Postfix(BuildingCollection __instance, OriginalPrefabs __state)
        {
                if (impactedByChangeCollections.Contains(__instance.gameObject?.name))
                {
                    __instance.m_prefabs = __state.prefabs;
                    __instance.m_replacedNames = __state.replacedNames;
                }
        }
    }
}