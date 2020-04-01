using Caliburn.Micro;
using NoDesk.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace NoDesk.ViewModels {
    class LoginViewModel : Screen {
        public ShellViewModel shellViewModel;
        UserDal userDAL = new UserDal();

        public Func<string> PasswordHandler { get; set; }

        public LoginViewModel(ShellViewModel shellViewModel) {
            this.shellViewModel = shellViewModel;
        }

        public string usernameInput { get; set; }
        public string passwordInput { get; set; }

        public void LoginButton() {
            List<User> users = userDAL.GetUserByUsername(usernameInput);
            if (users.Count > 0 && users[0].Password == passwordInput) { //user is found and password matches
                this.shellViewModel.LoggedUser = users[0];
                this.shellViewModel.ActivateItem(new DashboardViewModel(this.shellViewModel)); //show dashboard after login              
            } else {
                MessageBox.Show("Wrong username or password. Please try again.");
            }                 
        }
        
    }
}