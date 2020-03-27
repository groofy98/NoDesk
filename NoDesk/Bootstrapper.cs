using Caliburn.Micro;
using NoDesk.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NoDesk
{
    public class Bootstrapper : BootstrapperBase
    {
        public Bootstrapper()
        {        
        Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e) //open ShellViewModel on startup
        {
            DisplayRootViewFor<ShellViewModel>();
        }
    }
}
