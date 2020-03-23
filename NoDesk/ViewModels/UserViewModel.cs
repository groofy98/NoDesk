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

        public BindableCollection<User> Users
        {
            get { return _users; }
            set { _users = value;                
            }
        }


        public UserViewModel()
        {
            _users = new BindableCollection<User>(new UserDal().GetUsers());
        }
    }
}
