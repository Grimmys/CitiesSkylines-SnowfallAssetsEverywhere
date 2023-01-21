using ICities;
using UnityEngine.SceneManagement;

namespace SnowfallAssetsEverywhere
{
    public class SnowfallAssetsEverywhereLoader : ILoadingExtension
    {

        public void OnCreated(ILoading loading)
        {
        }

        public void OnLevelLoaded(LoadMode mode)
        {
            Utils.DebugLog($"Level loaded on mode: {mode}");
        }

        public void OnLevelUnloading()
        {
            Utils.DebugLog("Level is unloading");
            if (Utils.IsSnowfallInstalled())
            {
                if (Utils.GetNativeLevelScene() != Constants.SNOWFALL_LEVEL_SCENE)
                {
                    SceneManager.UnloadSceneAsync(Constants.SNOWFALL_LEVEL_SCENE);
                }
            }
            Utils.ResetState();
            BuildingCollectionAwakePatch.ResetState();
            CitizenCollectionAwakePatch.ResetState();
            NetCollectionAwakePatch.ResetState();
            PropCollectionAwakePatch.ResetState();
            TransportCollectionAwakePatch.ResetState();
            VehicleCollectionAwakePatch.ResetState();
        }

        public void OnReleased()
        {
        }
    }
}