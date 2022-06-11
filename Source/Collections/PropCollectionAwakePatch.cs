using HarmonyLib;

namespace SnowfallAssetsEverywhere
{
    [HarmonyPatch(typeof(PropCollection), "Awake")]
    public static class PropCollectionAwakePatch
    {
        public static bool Prefix(PropCollection __instance)
        {
            if (Utils.ShouldBeSkipped(__instance))
            {
                if (__instance?.gameObject.name != Constants.WINTER_BEAUTIFICATION)
                {
                    UnityEngine.Object.Destroy(__instance);
                    return false;
                }
            }
            return true;
        }
    }
}
