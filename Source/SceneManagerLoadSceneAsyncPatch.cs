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
            Utils.DebugLog("Imminent call to get level scene from SceneManager");
            if (sceneName == Utils.GetNativeLevelScene() && Utils.GetNativeLevelScene() != Constants.SNOWFALL_LEVEL_SCENE)
            {
                SceneManager.LoadSceneAsync(Constants.SNOWFALL_LEVEL_SCENE, mode);
            }
        }
    }

}