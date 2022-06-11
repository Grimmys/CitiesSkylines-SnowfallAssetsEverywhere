using HarmonyLib;

namespace SnowfallAssetsEverywhere
{
    [HarmonyPatch(typeof(VehicleCollection), "Awake")]
    public static class VehicleCollectionAwakePatch
    {
        public static bool Prefix(VehicleCollection __instance)
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
