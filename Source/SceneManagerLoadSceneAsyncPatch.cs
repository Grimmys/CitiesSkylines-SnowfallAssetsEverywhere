using UnityEngine.SceneManagement;
using HarmonyLib;
using System;
using UnityEngine;
using System.Collections.Generic;

namespace SnowfallAssetsEverywhere
{

    [HarmonyPatch(typeof(SceneManager), "LoadSceneAsync", new Type[] { typeof(string), typeof(LoadSceneMode) })]
    public static class SceneManagerLoadSceneAsyncPatch
    {
        public static AsyncOperation currentWinterOperation = null;
        public static Queue<string> nextWinterLevels = new Queue<string>();


        public static void Prefix(SceneManager __instance, string sceneName, LoadSceneMode mode)
        {
            if (currentWinterOperation != null && currentWinterOperation.isDone)
            {
                currentWinterOperation = null;
                if (nextWinterLevels.Count > 0)
                {
                    currentWinterOperation = SceneManager.LoadSceneAsync(nextWinterLevels.Dequeue(), LoadSceneMode.Additive);
                }

            }

            if (Utils.IsSnowfallInstalled())
            {
                if (sceneName == Utils.GetNativeLevelScene() && Utils.GetNativeLevelScene() != Constants.SNOWFALL_LEVEL_SCENE)
                {
                    currentWinterOperation = SceneManager.LoadSceneAsync(Constants.SNOWFALL_LEVEL_SCENE, mode);
                    if (Utils.IsAfterDarkInstalled())
                    {
                        nextWinterLevels.Enqueue(Constants.SNOWFALL_AFTERDARK_SCENE);
                    }
                    nextWinterLevels.Enqueue(Constants.SNOWFALL_SIGNUP_PACK_SCENE);
                }
            }
        }
    }
}