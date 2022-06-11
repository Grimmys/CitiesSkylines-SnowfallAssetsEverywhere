using ICities;
using CitiesHarmony.API;

namespace SnowfallAssetsEverywhere
{
    public class SnowfallAssetsEverywhereInfo : IUserMod
    {
        public string Name
        {
            get { return "Snowfall Assets on Every Map"; }
        }

        public string Description
        {
            get { return "Unlocks snowfall assets that are normally restricted to winter maps for every other maps as well."; }
        }

        public void OnEnabled()
        {
            HarmonyHelper.DoOnHarmonyReady(() => HarmonyPatcher.PatchAll());
        }

        public void OnDisabled()
        {
            if (HarmonyHelper.IsHarmonyInstalled) HarmonyPatcher.UnpatchAll();
        }
    }
}