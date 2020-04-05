using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using NoDesk.Dal;

namespace NoDesk.ViewModels
{
    class AddUserViewModel : Screen
    {
        public ShellViewModel shellViewModel;
        User user = new User();
        UserDal userDal = new UserDal();

        public string usernameInput { get; set; }
        public string emailInput { get; set; }

        public AddUserViewModel(ShellViewModel shellViewModel)
        {
            this.shellViewModel = shellViewModel;
        }

        //Setters for user values
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

        //--------------

        public void CancelButton()
        {
            //Returns back to userview
            shellViewModel.ActivateItem(new UserViewModel(shellViewModel));

        }
        public void AddUser()
        {
            List<User> users = userDal.GetUserByUsername(user.Username);
            List<User> emails = userDal.GetUserbyEmail(user.MailAddress);

            //Checks if username already exists
            if (users.Count > 0 && users[0].Username == user.Username)
            {
                MessageBox.Show("User already exists");
            }
            //Checks if email already exists
            else if (emails.Count > 0 && emails[0].MailAddress == user.MailAddress)
            {
                MessageBox.Show("Email address already exists");
            }

            //Adds user to database
            else
            { 
                user.AddUser(user);
                MessageBox.Show("User added!");
                shellViewModel.ActivateItem(new UserViewModel(shellViewModel));
            }



        }
    }
}

