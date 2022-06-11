using HarmonyLib;

namespace SnowfallAssetsEverywhere.Source.Collections
{
    [HarmonyPatch(typeof(NetCollection), "Awake")]
    public static class NetCollectionAwakePatch
    {
        public static bool Prefix(NetCollection __instance)
        {
            if (Utils.ShouldBeSkipped(__instance))
            {
                UnityEngine.Object.Destroy(__instance);
                return false;
            }
            return true;
        }
    }
}
