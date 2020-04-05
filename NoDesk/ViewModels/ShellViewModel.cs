using Caliburn.Micro;
using NoDesk.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoDesk.ViewModels
{
    public class ShellViewModel : Conductor<Object>
    {
        private User loggedUser;

        public ShellViewModel()
		{
            ActivateItem(new LoginViewModel(this));
		}

        public User LoggedUser {
            get { return loggedUser; }
            set {
                loggedUser = value;
                //update the LastLogin value in the database
                if (loggedUser != null)
                    new UserDal().UpdateLastLogin(LoggedUser);
                NotifyOfPropertyChange(() => CanShowDashboard);
                NotifyOfPropertyChange(() => CanShowUsers);
                NotifyOfPropertyChange(() => CanShowTickets);
            } //enable buttons when property 'loggedUser' is set
        }

        public void LogOut() {
            LoggedUser = null;
            ActivateItem(new LoginViewModel(this));
        }

		public void ShowUsers()
		{
			ActivateItem(new UserViewModel(this));
		}

		public void ShowDashboard()
		{
			ActivateItem(new DashboardViewModel(this));
		}

        public void ShowTickets() {
            ActivateItem(new IncidentTicketViewModel(this));
        }

        //caliburn convention: Can
        public bool CanShowTickets {
            get {
                if (LoggedUser != null) {
                    return true;
                }
                return false;
            }
        }

        public bool CanShowDashboard {
            get {
                if (LoggedUser != null) {
                        return true;
                }
                return false;
            }             
        }

        public bool CanShowUsers {
            get {
                if (LoggedUser != null &&LoggedUser.Type == UserType.Employee) {
                        return true;
                }
                return false;
            }              
        }
    }
}
