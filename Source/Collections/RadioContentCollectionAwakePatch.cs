using HarmonyLib;

namespace SnowfallAssetsEverywhere
{
    [HarmonyPatch(typeof(RadioContentCollection), "Awake")]
    public static class RadioContentCollectionAwakePatch
    {
        public static bool Prefix(RadioContentCollection __instance)
        {
            if (Utils.IsNonNativeComponent(__instance))
            {
                UnityEngine.Object.Destroy(__instance);
                return false;
            }
            return true;
        }
    }
}