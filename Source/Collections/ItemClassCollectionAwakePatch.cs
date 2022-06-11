using HarmonyLib;

namespace SnowfallAssetsEverywhere
{
    [HarmonyPatch(typeof(ItemClassCollection), "Awake")]
    public static class ItemClassCollectionAwakePatch
    {
        public static bool Prefix(ItemClassCollection __instance)
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
