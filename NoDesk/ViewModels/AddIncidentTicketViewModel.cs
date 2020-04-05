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
        public IncidentTicket IncidentTicket
        {
            get { return _incidentTicket; }
            set
            {
                _incidentTicket = value;
                NotifyOfPropertyChange(() => IncidentTicket);
            }
        }

        private BindableCollection<User> _users;
        public BindableCollection<User> Users
        {
            get { return _users; }
            set
            {
                _users = value;
            }
        }

        private BindableCollection<string> _types;
        public BindableCollection<string> TicketType
        {
            get { return _types; }
            set
            {
                _types = value;
            }
        }

        private BindableCollection<string> _deadlines;
        public BindableCollection<string> TicketDeadline
        {
            get { return _deadlines; }
            set
            {
                _deadlines = value;
            }
        }

        BindableCollection<string> _priority;
        public BindableCollection<string> TicketPriority
        {
            get { return _priority; }
            set
            {
                _priority = value;
            }
        }


        string _deadline;
        string _type;

        public AddIncidentTicketViewModel(ShellViewModel shellViewModel)
        {
            this.shellViewModel = shellViewModel;
            _incidentTicket = new IncidentTicket();
            _incidentTicket.Date = DateTime.Now;

            _users = new BindableCollection<User>(new UserDal().GetUsers());

            _types = new BindableCollection<string>();
            _types.Add("Software");
            _types.Add("Hardware");
            _types.Add("Service");

            _deadlines = new BindableCollection<string>();
            _deadlines.Add("7 days");
            _deadlines.Add("14 days");
            _deadlines.Add("28 days");
            _deadlines.Add("6 months");

            _priority = new BindableCollection<string>();
            _priority.Add("Low");
            _priority.Add("Medium");
            _priority.Add("High");
        }

        public DateTime IncidentDate
        {
            get { return _incidentTicket.Date; }
            set { _incidentTicket.Date = value;
                NotifyOfPropertyChange(() => CanSubmitTicket);
            }
        }

        public string IncidentSubject
        {
            get { return _incidentTicket.Subject; }
            set { _incidentTicket.Subject = value;
                NotifyOfPropertyChange(() => CanSubmitTicket);
            }
        }

        public string IncidentReportedBy
        {
            get { return _incidentTicket.By; }
            set { _incidentTicket.By = value;
                NotifyOfPropertyChange(() => CanSubmitTicket);
            }
        }

        public string IncidentPriority
        {
            get { return _incidentTicket.Priority; }
            set { _incidentTicket.Priority = value;
                NotifyOfPropertyChange(() => CanSubmitTicket);
            }
        }

        public string IncidentDeadline
        {
            get { return _deadline; }
            set {
                _deadline = value;
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
            get { return _type; }
            set
            {
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
            get { return _incidentTicket.Description; }
            set { _incidentTicket.Description = value;
                NotifyOfPropertyChange(() => CanSubmitTicket);
            }
        }

        public bool CanSubmitTicket
        {
            get {
                return (_incidentTicket.Date != null && _incidentTicket.Type != 0 && !string.IsNullOrWhiteSpace(_incidentTicket.By) && !string.IsNullOrWhiteSpace(_incidentTicket.Priority)
                  && _incidentTicket.Deadline != null && !string.IsNullOrWhiteSpace(_incidentTicket.Subject) && !string.IsNullOrWhiteSpace(_incidentTicket.Description)); 
            }
        }

        public void SubmitTicket(IncidentTicket incidentTicket)
        {
            _incidentTicket.SubmitTicket(_incidentTicket);

            MessageBox.Show("Ticket added.");

            shellViewModel.ActivateItem(new IncidentTicketViewModel(shellViewModel));
        }

        public void CancelTicket()
        {
            shellViewModel.ActivateItem(new IncidentTicketViewModel(shellViewModel));
        }
    }
}
