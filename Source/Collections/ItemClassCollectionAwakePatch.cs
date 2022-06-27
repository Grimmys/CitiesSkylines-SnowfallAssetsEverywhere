using HarmonyLib;

namespace SnowfallAssetsEverywhere
{
    [HarmonyPatch(typeof(ItemClassCollection), "Awake")]
    public static class ItemClassCollectionAwakePatch
    {
        public static bool Prefix(ItemClassCollection __instance)
        {
            // Don't skip Winter ItemClassCollection but the one loaded in second place to avoid error with RICO revisited mod.
            if (__instance.name == "Classes")
            {
                if (Utils.ItemClassesAlreadyLoaded())
                {
                    return false;
                } else
                {
                    Utils.SetItemClassesLoaded();
                }

            }
            return true;
        }
    }
}
