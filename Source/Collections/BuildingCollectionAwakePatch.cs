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

        private static readonly string[] relevantNonNativeCollections = { Constants.WINTER_BEAUTIFICATION, Constants.WINTER_MONUMENT, Constants.WINTER_GARBAGE, Constants.WINTER_INDUSTRIAL_FARMING, Constants.WINTER_EXPANSION_1,
                                                                          Constants.INDUSTRIAL_FARMING, Constants.EXPANSION_1, Constants.SUMMER_EXPANSION_7, Constants.WINTER_EXPANSION_7, Constants.PREORDER_PACK };
        private static readonly string[] impactedByChangeCollections = relevantNonNativeCollections.Concat(new []{Constants.INDUSTRIAL_FARMING, Constants.EXPANSION_1, Constants.WINTER_SIGNUP_PACK}).ToArray();

        private static readonly string[] garbageWinterBuildings = { "Snowdump" };
        private static readonly string[] industrialFarmingWinterBuildings = { "4x4_Greenhouse", "3x2_Greenhouse", "2x2_Greenhouse" };
        private static readonly string[] industrialFarmingSunnyBuildings = { "Farming 4x4 Farm", "Farming3x2", "Farming2x2" };
        private static readonly string[] parkWinterBuildings = { "Ski Lodge", "Skating Rink", "Public Firepit", "Cross-Country Skiing", "Ice Sculpture Park", "Sled_Hill", "Curling Park" };
        private static readonly string[] afterDarkExpansionWinterBuildings = { "2x2_winter_fishing_pier", "Snowmobile Track", "Ice_Fishing_Pond", "Ice Hockey Rink" };
        private static readonly string[] afterDarkExpansionSunnyBuildings = { "2x2_Jet_ski_rental", "2x8_FishingPier", "Skatepark", "Beachvolley Court", "DrivingRange" };
        private static readonly string[] preorderSunnyBuildings = { "Basketball Court", "bouncer_castle" };

        private static bool expansion7AlreadyLoaded = false;

        public static void ResetState()
        {
            expansion7AlreadyLoaded = false;
        }

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
            if (Utils.IsNonNativeComponent(__instance))
            {
                if (!relevantNonNativeCollections.Contains(__instance.gameObject?.name))
                {
                    UnityEngine.Object.Destroy(__instance);
                    return false;
                }
                __instance.m_replacedNames = null;
                if (__instance.gameObject?.name == Constants.WINTER_INDUSTRIAL_FARMING)
                {
                    __instance.m_prefabs = __instance.m_prefabs.Where(prefab => industrialFarmingWinterBuildings.Contains(prefab.name)).ToArray();
                }
                else if (__instance.gameObject?.name == Constants.INDUSTRIAL_FARMING)
                {
                    __instance.m_prefabs = __instance.m_prefabs.Where(prefab => industrialFarmingSunnyBuildings.Contains(prefab.name)).ToArray();
                }
                else if (__instance.gameObject?.name == Constants.WINTER_GARBAGE)
                {
                    __instance.m_prefabs = __instance.m_prefabs.Where(prefab => garbageWinterBuildings.Contains(prefab.name)).ToArray();
                }
                else if (__instance.gameObject?.name == Constants.WINTER_BEAUTIFICATION)
                {
                    __instance.m_prefabs = __instance.m_prefabs.Where(prefab => parkWinterBuildings.Contains(prefab.name)).ToArray();
                }
                else if (__instance.gameObject?.name == Constants.WINTER_EXPANSION_1)
                {
                    __instance.m_prefabs = __instance.m_prefabs.Where(prefab => afterDarkExpansionWinterBuildings.Contains(prefab.name)).ToArray();
                }
                else if (__instance.gameObject?.name == Constants.EXPANSION_1)
                {
                    __instance.m_prefabs = __instance.m_prefabs.Where(prefab => afterDarkExpansionSunnyBuildings.Contains(prefab.name)).ToArray();
                }
                else if (__instance.gameObject?.name == Constants.PREORDER_PACK)
                {
                    __instance.m_prefabs = __instance.m_prefabs.Where(prefab => preorderSunnyBuildings.Contains(prefab.name)).ToArray();
                }
            }
            else
            {
                if (__instance.gameObject?.name == Constants.INDUSTRIAL_FARMING)
                {
                    RemoveReplacesFor(__instance.m_replacedNames, "_Greenhouse$");
                }
                else if (__instance.gameObject?.name == Constants.WINTER_INDUSTRIAL_FARMING)
                {
                    RemoveReplacesFor(__instance.m_replacedNames, "^Farming");
                }
                else if (__instance.gameObject?.name == Constants.EXPANSION_1)
                {
                    RemoveReplacesFor(__instance.m_replacedNames, "^2x2_winter_fishing_pier$");
                }
                else if (__instance.gameObject?.name == Constants.WINTER_EXPANSION_1)
                {
                    RemoveReplacesFor(__instance.m_replacedNames, "^2x2_Jet_ski_rental$");
                }
                else if (__instance.gameObject?.name == Constants.WINTER_SIGNUP_PACK)
                {
                    RemoveReplacesFor(__instance.m_replacedNames, "^JapaneseGarden$");
                }
                else if (__instance.gameObject?.name == Constants.SIGNUP_PACK)
                {
                    RemoveReplacesFor(__instance.m_replacedNames, "^Snowman_Park$");
                }
                else if (__instance.gameObject?.name == Constants.WINTER_BEAUTIFICATION)
                {
                    RemoveReplacesFor(__instance.m_replacedNames, "^bouncer_castle$");
                }
                else if (__instance.gameObject?.name == Constants.PREORDER_PACK)
                {
                    RemoveReplacesFor(__instance.m_replacedNames, "^Sled_Hill$");
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