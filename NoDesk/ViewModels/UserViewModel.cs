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
        private string _filter = "Filter by email";
        private List<User> hidedUsers;

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

        public string Filter {
            get { return _filter; }
            set {
                _filter = value;
                FilterUsers();
            }
        }

        public UserViewModel(ShellViewModel shellViewModel) {
            this.shellViewModel = shellViewModel;

            Users = new BindableCollection<User>(new UserDal().GetUsers());
            hidedUsers = new List<User>();
        }

        public void AddNewUser() {
            shellViewModel.ActivateItem(new AddUserViewModel(shellViewModel));
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

        public void FilterUsers() {
            if (Filter == "Filter by email") {
                return;
            }

            if (Filter == "") {
                foreach (User user in hidedUsers) {
                    Users.Add(user);
                }

                hidedUsers = new List<User>();
                return;
            }

            List<User> removedUsers = new List<User>();

            if (hidedUsers.Count > 0 ) {
                foreach (User user in hidedUsers) {
                    if (user.MailAddress.ToUpper().Contains(Filter.ToUpper())) {
                        Users.Add(user);
                        removedUsers.Add(user);
                    }
                }

                if (removedUsers.Count > 0) {
                    foreach (User user in removedUsers) {
                        hidedUsers.Remove(user);
                    }
                }

                removedUsers = new List<User>();
            }

            foreach (User user in Users) {
                if (!user.MailAddress.ToUpper().Contains(Filter.ToUpper())) {
                    hidedUsers.Add(user);
                    removedUsers.Add(user);
                }
            }

            if (removedUsers.Count > 0) {
                foreach (User user in removedUsers) {
                    Users.Remove(user);
                }
            }
        }
    }
}
