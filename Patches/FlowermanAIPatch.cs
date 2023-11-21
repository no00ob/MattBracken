using HarmonyLib;
using UnityEngine;
using LC_API.BundleAPI;
using static UnityEngine.UI.DefaultControls;
using BepInEx.Configuration;
using System;

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
            Plugin.Instance.logger.LogDebug($"Eye1 {__instance.gameObject.transform.Find("FlowermanModel").Find("AnimContainer").GetChild(1).GetChild(0).GetChild(2).GetChild(2).GetChild(10).GetChild(0).GetChild(0).GetChild(1).gameObject.name}");
            Plugin.Instance.logger.LogDebug($"Eye2 {__instance.gameObject.transform.Find("FlowermanModel").Find("AnimContainer").GetChild(1).GetChild(0).GetChild(2).GetChild(2).GetChild(10).GetChild(0).GetChild(0).GetChild(2).gameObject.name}");
            Plugin.Instance.logger.LogDebug($"Eye3 {__instance.gameObject.transform.Find("FlowermanModel").Find("AnimContainer").GetChild(1).GetChild(0).GetChild(2).GetChild(2).GetChild(10).GetChild(0).GetChild(0).GetChild(3).gameObject.name}");

            __instance.gameObject.transform.Find("FlowermanModel").Find("AnimContainer").GetChild(1).GetChild(0).GetChild(2).GetChild(2).GetChild(10).GetChild(0).GetChild(0).GetChild(1).gameObject.SetActive(false);
            __instance.gameObject.transform.Find("FlowermanModel").Find("AnimContainer").GetChild(1).GetChild(0).GetChild(2).GetChild(2).GetChild(10).GetChild(0).GetChild(0).GetChild(2).gameObject.SetActive(false);
            __instance.gameObject.transform.Find("FlowermanModel").Find("AnimContainer").GetChild(1).GetChild(0).GetChild(2).GetChild(2).GetChild(10).GetChild(0).GetChild(0).GetChild(3).gameObject.SetActive(false);
            // Instantiate our model prefab
            GameObject val = GameObject.Instantiate(BundleLoader.GetLoadedAsset<GameObject>("assets/prefabs/Matt.prefab"), __instance.gameObject.transform);
            // Set our models scale and position
            val.transform.localScale = new Vector3(3f,3f,3f);
            val.transform.localPosition = Vector3.zero;//new Vector3(0f, 1.5f, 0f);

            // Change audio clips to our custom ones
            __instance.creatureAngerVoice.clip = BundleLoader.GetLoadedAsset<AudioClip>("assets/audio/angry.wav");
            __instance.crackNeckSFX = BundleLoader.GetLoadedAsset<AudioClip>("assets/audio/bye.wav");
            __instance.crackNeckAudio.clip = BundleLoader.GetLoadedAsset<AudioClip>("assets/audio/bye.wav");
            __instance.dieSFX = BundleLoader.GetLoadedAsset<AudioClip>("assets/audio/scream.wav");
            __instance.enemyType.overrideVentSFX = BundleLoader.GetLoadedAsset<AudioClip>("assets/audio/laugh.wav");
            __instance.enemyType.hitBodySFX = BundleLoader.GetLoadedAsset<AudioClip>("assets/audio/disgust.wav");
            __instance.enemyType.stunSFX = BundleLoader.GetLoadedAsset<AudioClip>("assets/audio/crying.wav");

            // Debuggin to figure out brackens other sounds.
            for (int i = 0; i < __instance.enemyBehaviourStates.Length; i++)
            {
                if (__instance.enemyBehaviourStates[i] != null)
                {
                    Plugin.Instance.logger.LogDebug($"EnemyBehaviourState {__instance.enemyBehaviourStates[i].name} of index {i} out of the total {__instance.enemyBehaviourStates.Length}");
                    if (__instance.enemyBehaviourStates[i].SFXClip == null)
                    {
                        Plugin.Instance.logger.LogDebug($"--does not contain SFXClip of any name");
                    }
                    else
                    {
                        Plugin.Instance.logger.LogDebug($"--contains SFXClip {__instance.enemyBehaviourStates[i].SFXClip} of name {__instance.enemyBehaviourStates[i].SFXClip.name}");
                    }
                    if (__instance.enemyBehaviourStates[i].VoiceClip == null)
                    {
                        Plugin.Instance.logger.LogDebug($"--does not contain VoiceClip of any name");
                    }
                    else
                    {
                        Plugin.Instance.logger.LogDebug($"--contains VoiceClip {__instance.enemyBehaviourStates[i].VoiceClip} of name {__instance.enemyBehaviourStates[i].VoiceClip.name}");
                    }
                }
                else
                {
                    Plugin.Instance.logger.LogDebug($"Behaviour state index of {i} is null or was not found!");
                }
            }

            if (__instance.enemyType != null)
            {
                if (__instance.enemyType.audioClips != null && __instance.enemyType.audioClips.Length > 0)
                {
                    Plugin.Instance.logger.LogDebug($"Enemy {__instance.gameObject.name} of type {__instance.enemyType.enemyName} contains total of {__instance.enemyType.audioClips.Length} audio clips");
                }
                else
                {
                    Plugin.Instance.logger.LogDebug($"Enemy {__instance.gameObject.name} of type {__instance.enemyType.enemyName} does not contain any audio clips or audio clips are null");
                }

                if (__instance.enemyType.overrideVentSFX != null)
                {
                    Plugin.Instance.logger.LogDebug($"Enemy {__instance.gameObject.name} of type {__instance.enemyType.enemyName} has the following overrideVentSFX {__instance.enemyType.overrideVentSFX.name}");
                }
                else
                {
                    Plugin.Instance.logger.LogDebug($"Enemy {__instance.gameObject.name} of type {__instance.enemyType.enemyName} does not have overrideVentSFX");
                }

                if (__instance.enemyType.hitBodySFX != null)
                {
                    Plugin.Instance.logger.LogDebug($"Enemy {__instance.gameObject.name} of type {__instance.enemyType.enemyName} has the following hitBodySFX {__instance.enemyType.hitBodySFX.name}");
                }
                else
                {
                    Plugin.Instance.logger.LogDebug($"Enemy {__instance.gameObject.name} of type {__instance.enemyType.enemyName} does not have hitBodySFX");
                }

                if (__instance.enemyType.hitEnemyVoiceSFX != null)
                {
                    Plugin.Instance.logger.LogDebug($"Enemy {__instance.gameObject.name} of type {__instance.enemyType.enemyName} has the following hitEnemyVoiceSFX {__instance.enemyType.hitEnemyVoiceSFX.name}");
                }
                else
                {
                    Plugin.Instance.logger.LogDebug($"Enemy {__instance.gameObject.name} of type {__instance.enemyType.enemyName} does not have hitEnemyVoiceSFX");
                }

                if (__instance.enemyType.deathSFX != null)
                {
                    Plugin.Instance.logger.LogDebug($"Enemy {__instance.gameObject.name} of type {__instance.enemyType.enemyName} has the following deathSFX {__instance.enemyType.deathSFX.name}");
                }
                else
                {
                    Plugin.Instance.logger.LogDebug($"Enemy {__instance.gameObject.name} of type {__instance.enemyType.enemyName} does not have deathSFX");
                }

                if (__instance.enemyType.stunSFX != null)
                {
                    Plugin.Instance.logger.LogDebug($"Enemy {__instance.gameObject.name} of type {__instance.enemyType.enemyName} has the following stunSFX {__instance.enemyType.stunSFX.name}");
                }
                else
                {
                    Plugin.Instance.logger.LogDebug($"Enemy {__instance.gameObject.name} of type {__instance.enemyType.enemyName} does not have stunSFX");
                }

                for (int i = 0; i < __instance.enemyType.audioClips.Length; i++)
                {
                    if (__instance.enemyType.audioClips[i] != null)
                    {
                        Plugin.Instance.logger.LogDebug($"--Audio clip of index {i} is {__instance.enemyType.audioClips[i].name}");
                    }
                    else
                    {
                        Plugin.Instance.logger.LogDebug($"--Audio clip of index {i} was not found or is null");
                    }
                }
            }
            else
            {
                Plugin.Instance.logger.LogDebug($"Enemy {__instance.gameObject.name} has no defined enemy type or enemy type is null");
            }
        }
    }
}
