using HarmonyLib;
using UnityEngine;

namespace no00ob.Mod.LethalCompany.MattBracken.Patches
{
    [HarmonyPatch(typeof(FlowermanAI))]
    internal class FlowermanAIPatch
    {
        [HarmonyPatch(nameof(FlowermanAI.Start))]
        [HarmonyPostfix]
        public static void ReplaceWithMatt(FlowermanAI __instance)
        {
            Plugin.Instance.logger.LogDebug($"MATT HAS SPAWNED!\n-\n-\n-\n-\n-\n-\n-\n-\n-\n-\n-\n-\n-\n-\n-\n-\n-\n-\n-\n-\n-\n-\n-");

            // Destroy the Flowerman's 3D models mesh renderer so we can get rid of the original 3D model
            UnityEngine.Object.Destroy(__instance.gameObject.transform.Find("FlowermanModel").Find("LOD1").gameObject.GetComponent<SkinnedMeshRenderer>());
            // Destroy the Flowerman's glowing eyes 3D models mesh renderers so we can get rid of the original 3D models as our model contains glowing eyes included
            __instance.gameObject.transform.Find("FlowermanModel").Find("AnimContainer").GetChild(1).GetChild(0).GetChild(2).GetChild(2).GetChild(10).GetChild(0).GetChild(0).GetChild(1).gameObject.SetActive(false);
            __instance.gameObject.transform.Find("FlowermanModel").Find("AnimContainer").GetChild(1).GetChild(0).GetChild(2).GetChild(2).GetChild(10).GetChild(0).GetChild(0).GetChild(2).gameObject.SetActive(false);
            __instance.gameObject.transform.Find("FlowermanModel").Find("AnimContainer").GetChild(1).GetChild(0).GetChild(2).GetChild(2).GetChild(10).GetChild(0).GetChild(0).GetChild(3).gameObject.SetActive(false);
            // Instantiate our model prefab
            GameObject val = GameObject.Instantiate((GameObject)Plugin.Instance.resources[0].LoadAsset("assets/prefabs/Matt.prefab"), __instance.gameObject.transform); //BundleLoader.GetLoadedAsset<GameObject>("assets/prefabs/Matt.prefab")
            // Set our models scale and position
            val.transform.localScale = new Vector3(3f,3f,3f);
            val.transform.localPosition = Vector3.zero;//new Vector3(0f, 1.5f, 0f);

            PlayAudioAnimationEvent enemyAudioEvents = __instance.gameObject.transform.Find("FlowermanModel").Find("AnimContainer").GetComponent<PlayAudioAnimationEvent>();

            // Change audio clips to our custom ones
            __instance.creatureAngerVoice.clip = (AudioClip)Plugin.Instance.resources[0].LoadAsset("assets/audio/angry.wav");//BundleLoader.GetLoadedAsset<AudioClip>("assets/audio/angry.wav");
            __instance.crackNeckSFX = (AudioClip)Plugin.Instance.resources[0].LoadAsset("assets/audio/bye.wav");
            __instance.crackNeckAudio.clip = (AudioClip)Plugin.Instance.resources[0].LoadAsset("assets/audio/bye.wav");
            __instance.dieSFX = (AudioClip)Plugin.Instance.resources[0].LoadAsset("assets/audio/scream.wav");
            __instance.enemyType.overrideVentSFX = (AudioClip)Plugin.Instance.resources[0].LoadAsset("assets/audio/pop.wav");
            __instance.enemyType.hitBodySFX = (AudioClip)Plugin.Instance.resources[0].LoadAsset("assets/audio/disgust.wav");
            __instance.enemyType.stunSFX = (AudioClip)Plugin.Instance.resources[0].LoadAsset("assets/audio/crying.wav");
            enemyAudioEvents.audioClip = (AudioClip)Plugin.Instance.resources[0].LoadAsset("assets/audio/laugh.wav");
            // no need to change this, it's just the random rustle sounds
            // enemyAudioEvents.randomClips = 
            // footsteps V
            enemyAudioEvents.randomClips2 = new AudioClip[4] { 
                (AudioClip)Plugin.Instance.resources[0].LoadAsset("assets/audio/step_goofy_1.wav"),
                (AudioClip)Plugin.Instance.resources[0].LoadAsset("assets/audio/step_goofy_2.wav"),
                (AudioClip)Plugin.Instance.resources[0].LoadAsset("assets/audio/step_goofy_3.wav"),
                (AudioClip)Plugin.Instance.resources[0].LoadAsset("assets/audio/step_goofy_4.wav")
            };

            //SoundTool.ReplaceAudioClip("Found1", (AudioClip)Plugin.Instance.resources[0].LoadAsset("assets/audio/laugh.wav"));
        }
    }
}
