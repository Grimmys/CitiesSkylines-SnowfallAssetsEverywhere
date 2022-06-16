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

        public static void ResetNativeLevelScene()
        {
            nativeLevelScene = null;
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

        public static bool ShouldBeSkipped(Component component)
        {
            return GetNativeLevelScene() != Constants.SNOWFALL_LEVEL_SCENE && (component?.gameObject?.transform?.parent?.gameObject?.name == Constants.WINTER_COLLECTIONS || component?.gameObject?.name == Constants.WINTER_EXPANSION_1 || component?.gameObject.name == Constants.WINTER_PREORDER_PACK);
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
    }
}
