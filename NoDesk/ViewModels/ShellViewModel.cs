using Caliburn.Micro;
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
                NotifyOfPropertyChange(() => CanShowDashboard);
                NotifyOfPropertyChange(() => CanShowUsers);
                NotifyOfPropertyChange(() => CanShowTickets);
            }
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



        public bool CanShowDashboard {
            get {
                if (LoggedUser == null) {
                    return false;
                } else {
                    return true;
                }
            } 
        }

        public bool CanShowTickets {
            get {
                if (LoggedUser == null) {
                    return false;
                } else {
                    return true;
                }
            }
        }

        public bool CanShowUsers {
            get {
                if (LoggedUser == null) {
                    return false;
                } else {
                    return true;
                }
            }
        }

        public void AddTicket()
        {
            ActivateItem(new AddIncidentTicketViewModel(this));
        }
    }
}
