using System.Linq;
using HarmonyLib;

namespace SnowfallAssetsEverywhere
{
    [HarmonyPatch(typeof(PropCollection), "Awake")]
    public static class PropCollectionAwakePatch
    {

        private static bool expansion7AlreadyLoaded = false;

        public static void ResetState()
        {
            expansion7AlreadyLoaded = false;
        }

        public static bool Prefix(PropCollection __instance)
        {
            if (Utils.IsNonNativeComponent(__instance))
            {
                if (__instance?.gameObject.name != Constants.WINTER_BEAUTIFICATION && __instance?.gameObject.name != Constants.PREORDER_PACK)
                {
                    UnityEngine.Object.Destroy(__instance);
                    return false;
                }
                __instance.m_replacedNames = null;
                if (__instance?.gameObject.name == Constants.PREORDER_PACK)
                {
                    __instance.m_prefabs = __instance.m_prefabs.Where(prefab => prefab.name == "Basketball Court Decal").ToArray();
                }
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
