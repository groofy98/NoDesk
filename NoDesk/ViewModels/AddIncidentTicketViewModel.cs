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
    class AddIncidentTicketViewModel : Screen
    {
        ShellViewModel shellViewModel;
        private IncidentTicket _incidentTicket;
        private BindableCollection<User> _users;
        private BindableCollection<string> _ticketType;
        private BindableCollection<string> _ticketDeadlines;
        BindableCollection<string> _ticketPriority;

        public AddIncidentTicketViewModel(ShellViewModel shellViewModel)
        {
            this.shellViewModel = shellViewModel;
            _incidentTicket = new IncidentTicket();

            Users = new BindableCollection<User>(new UserDal().GetUsers());

            _ticketType = new BindableCollection<string>();
            _ticketType.Add("Software");
            _ticketType.Add("Hardware");
            _ticketType.Add("Service");

            _ticketDeadlines = new BindableCollection<string>();
            _ticketDeadlines.Add("7 days");
            _ticketDeadlines.Add("14 days");
            _ticketDeadlines.Add("28 days");
            _ticketDeadlines.Add("6 months");

            _ticketPriority = new BindableCollection<string>();
            _ticketPriority.Add("Low");
            _ticketPriority.Add("Medium");
            _ticketPriority.Add("High");
        }

        //----Start setters for IncidentTicket properties----//
        public DateTime IncidentDate
        {
           
            set { _incidentTicket.Date = value;
                NotifyOfPropertyChange(() => CanSubmitTicket);
            }
        }

        public string IncidentSubject
        {
            set { _incidentTicket.Subject = value;
                NotifyOfPropertyChange(() => CanSubmitTicket);
            }
        }
        
        public string IncidentReportedBy
        {
            set { _incidentTicket.By = value;
                NotifyOfPropertyChange(() => CanSubmitTicket);
            }
        }

        public string IncidentPriority
        {
            set { _incidentTicket.Priority = value;
                NotifyOfPropertyChange(() => CanSubmitTicket);
            }
        }

        public string IncidentDeadline
        {
            set {

                //----Sets deadline to a date----//
                switch (value)
                {
                    case "7 days":
                        _incidentTicket.Deadline = _incidentTicket.Date.AddDays(7);
                        break;
                    case "14 days":
                        _incidentTicket.Deadline = _incidentTicket.Date.AddDays(14);
                        break;
                    case "28 days":
                        _incidentTicket.Deadline = _incidentTicket.Date.AddDays(28);
                        break;
                    case "6 months":
                        _incidentTicket.Deadline = _incidentTicket.Date.AddDays(182.6);
                        break;
                }
                NotifyOfPropertyChange(() => CanSubmitTicket);
            }
        }

        public string IncidentTicketType
        {
            set
            {
                //----Sets IncidentType enumeration----//
                switch (value)
                {
                    case "Software":
                        _incidentTicket.Type = IncidentType.Software;
                        break;
                    case "Hardware":
                        _incidentTicket.Type = IncidentType.Hardware;
                        break;
                    case "Service":
                        _incidentTicket.Type = IncidentType.Service;
                        break;
                }
                NotifyOfPropertyChange(() => CanSubmitTicket);
            }
        }

        public string IncidentDescription
        {
            set { _incidentTicket.Description = value;
                NotifyOfPropertyChange(() => CanSubmitTicket);
            }
        }

        //----End setters for IncidentTicket properties----//

        //----Start getters and setters for collections----//
        public BindableCollection<User> Users
        {
            get { return _users; }
            set
            {
                _users = value;
            }
        }

        public BindableCollection<string> TicketType
        {
            get { return _ticketType; }
            set
            {
                _ticketType = value;
            }
        }

        public BindableCollection<string> TicketDeadlines
        {
            get { return _ticketDeadlines; }
            set
            {
                _ticketDeadlines = value;
            }
        }

        public BindableCollection<string> TicketPriority
        {
            get { return _ticketPriority; }
            set
            {
                _ticketPriority = value;
            }
        }

        //----End getters and setters for collections----//

        public bool CanSubmitTicket
        {
            //----Checks if all fields in the form are filled, enables SubmitTicket button if true----//
            get {
                return (_incidentTicket.Date != null && _incidentTicket.Type != 0 && !string.IsNullOrWhiteSpace(_incidentTicket.By) && !string.IsNullOrWhiteSpace(_incidentTicket.Priority)
                  && _incidentTicket.Deadline != null && !string.IsNullOrWhiteSpace(_incidentTicket.Subject) && !string.IsNullOrWhiteSpace(_incidentTicket.Description)); 
            }
        }

        public void SubmitTicket(IncidentTicket incidentTicket)
        {
            //----Submits ticket and returns to IncidentTicketViewModel----//
            _incidentTicket.SubmitTicket(_incidentTicket);

            MessageBox.Show("Ticket added.");

            shellViewModel.ActivateItem(new IncidentTicketViewModel(shellViewModel));
        }

        public void CancelTicket()
        {
            //----Returns to IncidentTicketViewModel----//
            shellViewModel.ActivateItem(new IncidentTicketViewModel(shellViewModel));
        }
    }
}
