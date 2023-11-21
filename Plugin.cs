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
        private const string PLUGIN_GUID = "no00ob.Mod.LethalCompany.MattBracken";
        private const string PLUGIN_NAME = "Matt Bracken";
        private const string PLUGIN_VERSION = "1.0.0";

        private readonly Harmony harmony = new Harmony(PLUGIN_GUID);

        public static Plugin Instance;

        internal ManualLogSource logger;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            logger = BepInEx.Logging.Logger.CreateLogSource(PLUGIN_GUID);

            logger.LogInfo($"Plugin {PLUGIN_GUID} is loaded!");


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