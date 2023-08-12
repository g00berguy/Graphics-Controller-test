using ComputerInterface.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphicsController.ComputerInterface
{
    internal class CIEntry : IComputerModEntry
    {
        // This is the name that will be shown on the main menu
        public string EntryName => PluginInfo.Name;

        // This is the first view that is going to be shown if the user selects you mod
        // The Computer Interface mod will instantiate your view 
        public Type EntryViewType => typeof(CIVIew);
    }
}
