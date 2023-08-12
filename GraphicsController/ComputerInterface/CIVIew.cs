using ComputerInterface.ViewLib;
using ComputerInterface;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using BepInEx.Configuration;
using System.IO;
using GraphicsController;

namespace GraphicsController.ComputerInterface
{
    public class CIVIew : ComputerView
    {
        int GraphicInt;
        const string title = "ffffff";
        const string subsetting = "ffffff";
        private readonly UISelectionHandler selectionHandler;
        bool FirstLoad;
        string location;

        public CIVIew()
        {
            selectionHandler = new UISelectionHandler(EKeyboardKey.Up, EKeyboardKey.Down, EKeyboardKey.Enter);
            selectionHandler.MaxIdx = 1;
            selectionHandler.ConfigureSelectionIndicator($"<color=#{subsetting}>></color> ", "", "  ", "");
        }

        // This is called when your view is opened
        public override void OnShow(object[] args)
        {
            base.OnShow(args);
            if (FirstLoad)
            {
                if (Plugin.instance.setting1.Value >= 0)
                {
                    GraphicInt = Plugin.instance.setting1.Value;
                }
                FirstLoad = false;
            }
            UpdateText();
        }

        public void UpdateText()
        {
            SetText(str =>
            {
                if (FirstLoad)
                {
                    location = Directory.GetCurrentDirectory();
                    ConfigFile configFile = new ConfigFile($@"{location}\BepInEx\config\Graphics Controller.cfg", true);
                    ConfigEntry<int> setting1 = configFile.Bind("Graphics Controller", "Graphics Quality", 0, "The graphics quality that is used on launch\nPick a number 1-9 (0 for default)");

                    if (setting1.Value >= 0 && setting1.Value <= 9)
                    {
                        GraphicInt = setting1.Value;
                    }
                    FirstLoad = false;
                }
                str.BeginCenter();
                str.MakeBar('-', SCREEN_WIDTH, 0, "ffffff10");
                str.AppendClr("|| Graphics Controller ||", title).EndColor().AppendLine();
                str.AppendLine(PluginInfo.Version);
                str.MakeBar('-', SCREEN_WIDTH, 0, "ffffff10");
                str.EndAlign().AppendLines(1);
                str.MakeBar(' ', SCREEN_WIDTH, 0, "ffffff10");
                str.AppendLine($"Graphics: {GraphicInt}\n^PRESS ENTER TO CONFIRM!^");
                str.AppendLines(1);
                str.MakeBar(' ', SCREEN_WIDTH, 0, "ffffff10");
            });
        }

        // You can do something on keypresses by overriding "OnKeyPressed"
        // It gets an EKeyboardKey passed as a parameter which wraps the old character string
        public override void OnKeyPressed(EKeyboardKey key)
        {
            switch (key)
            {
                case EKeyboardKey.Back:
                    // "ReturnToMainMenu" will switch to the main menu again
                    ReturnToMainMenu();
                    break;
                case EKeyboardKey.Right:
                    if (GraphicInt < 9)
                    {
                        GraphicInt += 1;
                        UpdateText();
                    }
                    break;
                case EKeyboardKey.Left:
                    if (GraphicInt > 0)
                    {
                        GraphicInt -= 1;
                        UpdateText();
                    }
                    break;
                case EKeyboardKey.Enter:
                    ChangeGraphics(GraphicInt);
                    break;
            }
        }

        void ChangeGraphics(int gr)
        {
            QualitySettings.masterTextureLimit = gr;
        }
    }
}