using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoDesk.ViewModels
{
    class DashboardViewModel
    {
        ShellViewModel shellViewModel;

        public DashboardViewModel(ShellViewModel shellViewModel) {
            this.shellViewModel = shellViewModel;
        }
    }
}
