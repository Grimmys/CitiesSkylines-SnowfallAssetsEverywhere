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
            // Reset mesh if necessary to force reinitialize of the element and avoid rendering issues with LOD mods. 
            if (__instance.m_lodLocations == null)
            {
                __instance.m_mesh = null;
            }
        }
    }
}
