using System.IO;
using System;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using no00ob.Mod.LethalCompany.MattBracken.Patches;
using UnityEngine;

namespace no00ob.Mod.LethalCompany.MattBracken
{
    [BepInPlugin(PLUGIN_GUID, PLUGIN_NAME, PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        private const string PLUGIN_GUID = "MattBracken";
        private const string PLUGIN_NAME = "Matt Bracken";
        private const string PLUGIN_VERSION = "1.1.0";

        private readonly Harmony harmony = new Harmony(PLUGIN_GUID);

        public static Plugin Instance;

        internal ManualLogSource logger;

        private static string[] resourceFileNames = new string[2] { "Matt", "mattbracken" };
        internal AssetBundle[] resources;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            logger = BepInEx.Logging.Logger.CreateLogSource(PLUGIN_GUID);

            logger.LogInfo($"Plugin {PLUGIN_GUID} is loaded!");

            resources = new AssetBundle[resourceFileNames.Length - 1];

            for (int i = 1; i < resourceFileNames.Length; i++)
            {
                // build absolute path
                var path = Path.Combine(Environment.CurrentDirectory, "BepInEx", "plugins", resourceFileNames[0], resourceFileNames[i]);

                logger.LogDebug($"Loading AssetBundle {resourceFileNames[i]} from {path}");

                resources[i - 1] = AssetBundle.LoadFromFile(path);

                if (resources != null)
                {
                    logger.LogDebug($"Succesfully loaded AssetBundle {resourceFileNames[i]}!");
                }
                else
                {
                    logger.LogError($"Failed to load AssetBundle {resourceFileNames[i]} from path {path}!");
                }
            }

            PatchAll();
        }

        private void PatchAll()
        {
            harmony.PatchAll(typeof(Plugin));
            harmony.PatchAll(typeof(FlowermanAIPatch));
            harmony.PatchAll(typeof(TerminalPatch));
        }
    }
}