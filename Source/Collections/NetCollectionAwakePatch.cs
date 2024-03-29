﻿using HarmonyLib;

namespace SnowfallAssetsEverywhere
{
    [HarmonyPatch(typeof(NetCollection), "Awake")]
    public static class NetCollectionAwakePatch
    {

        private static bool expansion7AlreadyLoaded = false;

        public static void ResetState()
        {
            expansion7AlreadyLoaded = false;
        }

        public static bool Prefix(NetCollection __instance)
        {
            if (Utils.IsNonNativeComponent(__instance))
            {
                UnityEngine.Object.Destroy(__instance);
                return false;
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
            return true;
        }
    }
}
