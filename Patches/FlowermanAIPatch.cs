using HarmonyLib;
using UnityEngine;
using LC_API.BundleAPI;

namespace no00ob.Mod.LethalCompany.MattBracken.Patches
{
    [HarmonyPatch(typeof(FlowermanAI))]
    internal class FlowermanAIPatch
    {
        [HarmonyPatch(nameof(FlowermanAI.Start))]
        [HarmonyPostfix]
        public static void ReplaceWithMatt(FlowermanAI __instance)
        {
            // Destroy the Flowerman's 3D models mesh renderer so we can get rid of the original 3D model
            UnityEngine.Object.Destroy(__instance.gameObject.transform.Find("FlowermanModel").Find("LOD1").gameObject.GetComponent<SkinnedMeshRenderer>());
            // Destroy the Flowerman's glowing eyes 3D models mesh renderers so we can get rid of the original 3D models as our model contains glowing eyes included
            UnityEngine.Object.Destroy(__instance.gameObject.transform.Find("FlowermanModel").Find("AnimContainer").GetChild(1).GetChild(0).GetChild(2).GetChild(2).GetChild(10).GetChild(0).GetChild(0).GetChild(1).gameObject.GetComponent<SkinnedMeshRenderer>());
            UnityEngine.Object.Destroy(__instance.gameObject.transform.Find("FlowermanModel").Find("AnimContainer").GetChild(1).GetChild(0).GetChild(2).GetChild(2).GetChild(10).GetChild(0).GetChild(0).GetChild(2).gameObject.GetComponent<SkinnedMeshRenderer>());
            // Instantiate our model prefab
            GameObject val = GameObject.Instantiate(BundleLoader.GetLoadedAsset<GameObject>("assets/prefabs/Matt.prefab"), __instance.gameObject.transform);
            // Set our models scale and position
            val.transform.localScale = new Vector3(3f,3f,3f);
            val.transform.localPosition = Vector3.zero;//new Vector3(0f, 1.5f, 0f);

            // Change audio clips to our custom ones
            __instance.creatureAngerVoice.clip = BundleLoader.GetLoadedAsset<AudioClip>("assets/audio/angry.wav");
            __instance.crackNeckSFX = BundleLoader.GetLoadedAsset<AudioClip>("assets/audio/bye.wav");
            __instance.crackNeckAudio.clip = BundleLoader.GetLoadedAsset<AudioClip>("assets/audio/bye.wav");
            __instance.dieSFX = BundleLoader.GetLoadedAsset<AudioClip>("assets/audio/crying.wav");

            // Debuggin to figure out brackens other sounds.
            for (int i = 0; i < __instance.enemyBehaviourStates.Length; i++)
            {
                Plugin.Instance.logger.LogDebug($"EnemyBehaviourState {__instance.enemyBehaviourStates[i].name} of index {i}");
                Plugin.Instance.logger.LogDebug($"--contains SFXClip {__instance.enemyBehaviourStates[i].SFXClip} of name {__instance.enemyBehaviourStates[i].SFXClip.name}");
                Plugin.Instance.logger.LogDebug($"--contains the following VoiceClip {__instance.enemyBehaviourStates[i].VoiceClip} of name {__instance.enemyBehaviourStates[i].VoiceClip.name}");
            }
            
        }
    }
}
