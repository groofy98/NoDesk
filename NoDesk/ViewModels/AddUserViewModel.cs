using Caliburn.Micro;
using NoDesk.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace NoDesk.ViewModels
{
    class AddUserViewModel : Screen
    {
        public ShellViewModel shellViewModel;
        User user = new User();

        public AddUserViewModel(ShellViewModel shellViewModel)
        {
            this.shellViewModel = shellViewModel;
        }

        public string FirstName
        {
            set { user.FirstName = value; }
        }

        public string LastName
        {
            set { user.LastName = value; }
        }

        public string Username
        {
            set { user.Username = value; }
        }

        public string Password
        {
            set { user.Password = value; }
        }
        public UserType Type
        {
            set { user.Type = value; }
        }

        public string Mail
        {
            set { user.MailAddress = value; }
        }

        public int PhoneNumber
        {
            set { user.PhoneNumber = value; }
        }

        public string Location
        {
            set { user.Location = value; }
        }

        public void CancelButton()
        {

            shellViewModel.ActivateItem(new UserViewModel(shellViewModel));

        }
        public void AddUser()
        {
            user.AddUser(user);
            MessageBox.Show("User added!");
            shellViewModel.ActivateItem(new UserViewModel(shellViewModel));

        }
    }
}

