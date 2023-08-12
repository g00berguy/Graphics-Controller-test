using ComputerInterface.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Zenject;

namespace GraphicsController.ComputerInterface
{
    internal class MainInstaller : Installer
    {
        public override void InstallBindings()
        {
            // Bind your mod entry like this
            Container.Bind<IComputerModEntry>().To<CIEntry>().AsSingle();
        }
    }
}
