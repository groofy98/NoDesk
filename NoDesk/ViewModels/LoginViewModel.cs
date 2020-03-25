using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoDesk.ViewModels {
    class LoginViewModel : Screen {
        public ShellViewModel shellViewModel;

        public LoginViewModel(ShellViewModel shellViewModel) {
            this.shellViewModel = shellViewModel;
        }

        public void LoginButton() {
            this.shellViewModel.LoggedUser = new User();
            this.shellViewModel.ActivateItem(new DashboardViewModel(this.shellViewModel));
        }
    }
}
