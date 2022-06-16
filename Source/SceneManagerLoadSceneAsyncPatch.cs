using UnityEngine.SceneManagement;
using HarmonyLib;
using System;

namespace SnowfallAssetsEverywhere
{
    [HarmonyPatch(typeof(SceneManager), "LoadSceneAsync", new Type[] { typeof(string), typeof(LoadSceneMode) })]
    public static class SceneManagerLoadSceneAsyncPatch
    {
        public static void Prefix(SceneManager __instance, string sceneName, LoadSceneMode mode)
        {
            if (Utils.IsSnowfallInstalled())
            {
                if (sceneName == Utils.GetNativeLevelScene() && Utils.GetNativeLevelScene() != Constants.SNOWFALL_LEVEL_SCENE)
                {
                    SceneManager.LoadSceneAsync(Constants.SNOWFALL_LEVEL_SCENE, mode);
                    if (Utils.IsAfterDarkInstalled())
                    {
                        SceneManager.LoadSceneAsync(Constants.SNOWFALL_AFTERDARK_SCENE, LoadSceneMode.Additive);
                    }
                    SceneManager.LoadSceneAsync(Constants.SNOWFALL_SIGNUP_PACK_SCENE, LoadSceneMode.Additive);
                }
            }
        }
    }
}