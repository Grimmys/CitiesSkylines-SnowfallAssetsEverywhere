using UnityEngine;
using ColossalFramework;
using ColossalFramework.PlatformServices;

namespace SnowfallAssetsEverywhere
{
    public static class Utils
    {
        public static void DebugLog(string message)
        {
            Debug.Log($"[{Constants.MOD_NAME}] {message}");
        }

        private static string nativeLevelScene;
        private static bool itemClassesLoaded = false;

        public static void ResetState()
        {
            nativeLevelScene = null;
            itemClassesLoaded = false;
        }

        public static string GetNativeLevelScene()
        {
            if (nativeLevelScene != null)
            {
                return nativeLevelScene;
            }
            var simulationManager = Singleton<SimulationManager>.instance;
            var mapEnvironment = simulationManager?.m_metaData?.m_environment;
            if (mapEnvironment != null)
            {
                nativeLevelScene = $"{mapEnvironment}Prefabs";
            }
            return nativeLevelScene;
        }

        public static bool ItemClassesAlreadyLoaded()
        {
            return itemClassesLoaded;
        }

        public static void SetItemClassesLoaded()
        {
            itemClassesLoaded = true;
        }

        public static bool ShouldBeSkipped(Component component)
        {
            var nativeLevelScene = GetNativeLevelScene();
            if (nativeLevelScene != Constants.SNOWFALL_LEVEL_SCENE) {
                return (component?.gameObject?.transform?.parent?.gameObject?.name == Constants.WINTER_COLLECTIONS || component?.gameObject?.name == Constants.WINTER_EXPANSION_1 || component?.gameObject?.name == Constants.WINTER_EXPANSION_7 || component?.gameObject.name == Constants.WINTER_PREORDER_PACK);
            } else {
                return (component?.gameObject?.transform?.parent?.gameObject?.name == Constants.SUNNY_COLLECTIONS || component?.gameObject?.name == Constants.PREORDER_PACK || component?.gameObject?.name == Constants.EXPANSION_1 || component?.gameObject?.name == Constants.SUMMER_EXPANSION_7);
            };
        }

        public static void LogCollectionComponent(Component component, string collection)
        {
            DebugLog($"{collection}: {component?.gameObject?.name}");
            DebugLog($"{collection}'s parent: {component?.gameObject?.transform?.parent?.name}");
        }
        public static bool IsSnowfallInstalled()
        {
            return PlatformService.IsDlcInstalled((uint)SteamHelper.DLC.SnowFallDLC);
        }

        public static bool IsAfterDarkInstalled()
        {
            return PlatformService.IsDlcInstalled((uint)SteamHelper.DLC.AfterDarkDLC);
        }

        public static bool IsIndustryInstalled()
        {
            return PlatformService.IsDlcInstalled((uint)SteamHelper.DLC.IndustryDLC);
        }
    }
}
