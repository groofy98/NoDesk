using Caliburn.Micro;
using NoDesk.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoDesk.ViewModels
{
     public class UserViewModel : Screen
    {
        private BindableCollection<User> _users;
        private ShellViewModel shellViewModel;

        public BindableCollection<User> Users
        {
            get { return _users; }
            set { _users = value;                
            }
        }


        public UserViewModel(ShellViewModel shellViewModel)
        {
            Users = new BindableCollection<User>(new UserDal().GetUsers());
            this.shellViewModel = shellViewModel;
        }

        public void AddNewUser() {

        }
    }
}
