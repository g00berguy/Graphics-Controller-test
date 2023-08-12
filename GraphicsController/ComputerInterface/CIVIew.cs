using ComputerInterface.ViewLib;
using ComputerInterface;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GraphicsController.ComputerInterface
{
    public class CIVIew : ComputerView
    {
        int GraphicInt;
        const string title = "ffffff";
        const string subsetting = "ffffff";
        private readonly UISelectionHandler selectionHandler;

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
            UpdateText();
        }

        public void UpdateText()
        {
            SetText(str =>
            {
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