using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HarmonyLib;

namespace SnowfallAssetsEverywhere
{
    [HarmonyPatch(typeof(TreeInfo), "InitializePrefab")]
    class TreeInfoInitializePrefabPatch
    {
        public static void Prefix(TreeInfo __instance)
        {
            if (__instance.m_lodLocations == null)
            {
                __instance.m_mesh = null;
            }
        }
    }
}
