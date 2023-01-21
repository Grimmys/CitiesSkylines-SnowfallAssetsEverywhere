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
        public static AsyncOperation currentLoadingSceneOperation = null;
        public static Queue<string> nextLevelsToBeLoaded = new Queue<string>();

        public static void Prefix(SceneManager __instance, string sceneName, LoadSceneMode mode)
        {
            if (currentLoadingSceneOperation != null && currentLoadingSceneOperation.isDone)
            {
                currentLoadingSceneOperation = null;
                if (nextLevelsToBeLoaded.Count > 0)
                {
                    currentLoadingSceneOperation = SceneManager.LoadSceneAsync(nextLevelsToBeLoaded.Dequeue(), LoadSceneMode.Additive);
                }

            }

            if (Utils.IsSnowfallInstalled())
            {
                if (sceneName == Utils.GetNativeLevelScene())
                {
                    if (Utils.GetNativeLevelScene() != Constants.SNOWFALL_LEVEL_SCENE)
                    {
                        currentLoadingSceneOperation = SceneManager.LoadSceneAsync(Constants.SNOWFALL_LEVEL_SCENE, mode);
                        if (Utils.IsAfterDarkInstalled())
                        {
                            nextLevelsToBeLoaded.Enqueue(Constants.SNOWFALL_AFTERDARK_SCENE);
                        }
                        if (Utils.IsIndustryInstalled())
                        {
                            nextLevelsToBeLoaded.Enqueue(Constants.SNOWFALL_INDUSTRY_SCENE);
                        }
                        nextLevelsToBeLoaded.Enqueue(Constants.SNOWFALL_SIGNUP_PACK_SCENE);
                    } else
                    {
                        currentLoadingSceneOperation = SceneManager.LoadSceneAsync(Constants.SUMMER_LEVEL_SCENE, mode);
                        if (Utils.IsAfterDarkInstalled())
                        {
                            nextLevelsToBeLoaded.Enqueue(Constants.AFTERDARK_SCENE);
                        }
                        if (Utils.IsIndustryInstalled())
                        {
                            nextLevelsToBeLoaded.Enqueue(Constants.INDUSTRY_SCENE);
                        }
                        nextLevelsToBeLoaded.Enqueue(Constants.SIGNUP_PACK_SCENE);
                        nextLevelsToBeLoaded.Enqueue(Constants.PREORDER_PACK_SCENE);
                    }
                }
            }
        }
    }
}