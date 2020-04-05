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
    class UserTicketsViewModel : Screen
    {
        ShellViewModel shellViewModel;
        private BindableCollection<User> _users;
        private BindableCollection<IncidentTicket> _tickets;
        private string _username;
        private IncidentTicket _selectedIncidentTicket;
        

        public UserTicketsViewModel(ShellViewModel shellViewModel)
        {
            this.shellViewModel = shellViewModel;
            Users = new BindableCollection<User>(new UserDal().GetUsers());
        }

        public BindableCollection<User> Users
        {
            get { return _users; }
            set { _users = value; }
        }

        public BindableCollection<IncidentTicket> Tickets
        {
            get { return _tickets; }
            set
            {
                _tickets = value;
                NotifyOfPropertyChange(() => Tickets);
            }
        }

        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                Tickets = new BindableCollection<IncidentTicket>(new TicketDal().GetTicketsByUsername(_username));
                NotifyOfPropertyChange(() => Username);
            }
        }

        public IncidentTicket SelectedIncidentTicket
        {
            get { return _selectedIncidentTicket; }
            set
            {
                _selectedIncidentTicket = value;
                NotifyOfPropertyChange(() => SelectedIncidentTicket);
            }
        }

        public void BackButton()
        {
            shellViewModel.ActivateItem(new IncidentTicketViewModel(shellViewModel));
        }
    }
}
