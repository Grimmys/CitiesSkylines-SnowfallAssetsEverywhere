using HarmonyLib;

namespace SnowfallAssetsEverywhere
{
    [HarmonyPatch(typeof(TransportCollection), "Awake")]
    public static class TransportCollectionAwakePatch
    {

        private static bool expansion7AlreadyLoaded = false;

        public static void ResetState()
        {
            expansion7AlreadyLoaded = false;
        }

        public static bool Prefix(TransportCollection __instance)
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
