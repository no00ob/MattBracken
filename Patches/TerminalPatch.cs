using HarmonyLib;
using UnityEngine;
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
            __instance.enemyFiles[1].displayVideo = (VideoClip)Plugin.Instance.resources[0].LoadAsset("assets/videos/matt.m4v"); //BundleLoader.GetLoadedAsset<VideoClip>("assets/videos/matt.m4v");
        }
    }
}
