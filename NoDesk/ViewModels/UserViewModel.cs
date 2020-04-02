using Caliburn.Micro;
using NoDesk.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NoDesk.ViewModels
{
    public class UserViewModel : Screen {
        private BindableCollection<User> _users;
        private User _selectedUser;
        private ShellViewModel shellViewModel;

        public BindableCollection<User> Users
        {
            get { return _users; }
            set { _users = value;
                NotifyOfPropertyChange(() => Users);
            }
        }

        public User SelectedUser {
            get { return _selectedUser; }
            set {
                _selectedUser = value;
                NotifyOfPropertyChange(() => SelectedUser);
                NotifyOfPropertyChange(() => CanSaveUser);
                NotifyOfPropertyChange(() => CanDeleteUser);
            }
        }

        public UserViewModel(ShellViewModel shellViewModel) {
            this.shellViewModel = shellViewModel;

            Users = new BindableCollection<User>(new UserDal().GetUsers());
        }

        public void AddNewUser() {

        }

        public void SaveUser() {
            UserDal userDal = new UserDal();

            foreach (User user in Users) {
                userDal.UpdateUser(user);
            }

            Users = new BindableCollection<User>(userDal.GetUsers());
            SelectedUser = null;
        }

        public void DeleteUser() {
            if (MessageBox.Show("Are you sure you want to delete this user?",
                "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes) {
                UserDal userDal = new UserDal();

                userDal.DeleteUser(SelectedUser);

                Users = new BindableCollection<User>(userDal.GetUsers());
            }

            SelectedUser = null;
        }

        public bool CanSaveUser {
            get { return !(SelectedUser == null); }
        }

        public bool CanDeleteUser {
            get { return !(SelectedUser == null); }
        }
    }
}
