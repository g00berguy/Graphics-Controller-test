using BepInEx;
using BepInEx.Bootstrap;
using BepInEx.Configuration;
using GraphicsController.ComputerInterface;
using System;
using System.IO;
using System.Linq;
using UnityEngine;
using Utilla;

namespace GraphicsController
{
    /// <summary>
    /// This is your mod's main class.
    /// </summary>

    /* This attribute tells Utilla to look for [ModdedGameJoin] and [ModdedGameLeave] */
    [ModdedGamemode]
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        bool inRoom;
        string location;
        ConfigFile configFile;
        public ConfigEntry<int> setting1;
        void Start()
        {
            location = Directory.GetCurrentDirectory();
            configFile = new ConfigFile($@"{location}\BepInEx\config\Graphics Controller.cfg", true);
            setting1 = configFile.Bind("Graphics Controller", "Graphics Quality", 0, "The graphics quality that is used on launch\nPick a number 1-9 (0 for default)");
            if (setting1.Value >= 1 && setting1.Value <= 9)
            {
                ChangeGraphics(setting1.Value);
            }
            Bepinject.Zenjector.Install<MainInstaller>().OnProject();
            Utilla.Events.GameInitialized += OnGameInitialized;
        }

        void Awake()
        {
            
        }

        void ChangeGraphics(int gr)
        {
            QualitySettings.masterTextureLimit = gr;
        }

        void OnEnable()
        {
            /* Set up your mod here */
            /* Code here runs at the start and whenever your mod is enabled*/

            HarmonyPatches.ApplyHarmonyPatches();
        }

        void OnDisable()
        {
            /* Undo mod setup here */
            /* This provides support for toggling mods with ComputerInterface, please implement it :) */
            /* Code here runs whenever your mod is disabled (including if it disabled on startup)*/

            HarmonyPatches.RemoveHarmonyPatches();
        }

        void OnGameInitialized(object sender, EventArgs e)
        {
            
        }

        void Update()
        {
            /* Code here runs every frame when the mod is enabled */
        }
    }
}
