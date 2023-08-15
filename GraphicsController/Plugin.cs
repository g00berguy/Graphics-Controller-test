using BepInEx;
using BepInEx.Bootstrap;
using BepInEx.Configuration;
using Bepinject;
using GraphicsController.ComputerInterface;
using System;
using System.IO;
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
        string location;
        ConfigFile configFile;
        public ConfigEntry<int> setting1;
        void Awake()
        {
            Zenjector.Install<MainInstaller>().OnProject();

            location = Directory.GetCurrentDirectory();
            configFile = new ConfigFile($@"{location}\BepInEx\config\Graphics Controller.cfg", true);
            setting1 = configFile.Bind("Graphics Controller", "Graphics Quality", 0, "The graphics quality that is used on launch\nPick a number 1-9 (0 for default)");
            if (setting1.Value >= 1)
            {
                ChangeGraphics(setting1.Value);
            }
            Utilla.Events.GameInitialized += OnGameInitialized;
            Application.quitting += Quitting;
        }

        void Quitting()
        {
            setting1.Value = QualitySettings.masterTextureLimit;
            configFile.Save();
        }

        void ChangeGraphics(int gr)
        {
            QualitySettings.masterTextureLimit = gr;
            setting1.Value = gr;
        }

        void OnEnable()
        {
            ChangeGraphics(setting1.Value);

            HarmonyPatches.ApplyHarmonyPatches();
        }

        void OnDisable()
        {
            ChangeGraphics(0);

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
