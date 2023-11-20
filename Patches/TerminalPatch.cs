using HarmonyLib;
using LC_API.BundleAPI;
using UnityEngine.Video;

namespace no00ob.Mod.LethalCompany.MattBracken.Patches
{
    [HarmonyPatch(typeof(Terminal))]
    internal class TerminalPatch
    {
        [HarmonyPatch("Awake")]
        [HarmonyPostfix]
        public static void ReplaceBrackenTerminalVideo(Terminal __instance)
        {
            __instance.enemyFiles[1].displayVideo = BundleLoader.GetLoadedAsset<VideoClip>("assets/videos/matt.m4v");
        }
    }
}
