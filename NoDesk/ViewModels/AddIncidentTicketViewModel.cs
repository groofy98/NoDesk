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
    class AddIncidentTicketViewModel : UserControl
    {
        ShellViewModel shellViewModel;
        IncidentTicket _incidentTicket;
        BindableCollection<User> _users;
        BindableCollection<string> _types;
        BindableCollection<string> _deadlines;
        BindableCollection<string> _priority;


        string _deadline;
        string _type;

        public AddIncidentTicketViewModel(ShellViewModel shellViewModel)
        {
            this.shellViewModel = shellViewModel;
            _incidentTicket = new IncidentTicket();

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
            get { return _types; }
            set
            {
                _types = value;
            }
        }

        public BindableCollection<string> TicketDeadline
        {
            get { return _deadlines; }
            set
            {
                _deadlines = value;
            }
        }

        public BindableCollection<string> TicketPriority
        {
            get { return _priority; }
            set
            {   
                _priority = value;
            }
        }

        public DateTime IncidentDate
        {
            get { return _incidentTicket.Date; }
            set { _incidentTicket.Date = value; }
        }

        public string IncidentSubject
        {
            get { return _incidentTicket.Subject; }
            set { _incidentTicket.Subject = value; }
        }

        public string IncidentReportedBy
        {
            get { return _incidentTicket.By; }
            set { _incidentTicket.By = value; }
        }

        public string IncidentPriority
        {
            get { return _incidentTicket.Priority; }
            set { _incidentTicket.Priority = value; }
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
            }
        }

        public string IncidentTicketType
        {
            get { return _type; }
            set
            {
                _type = value;
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
            }
        }

        public string IncidentDescription
        {
            get { return _incidentTicket.Description; }
            set { _incidentTicket.Description = value; }
        }

        public void SubmitTicket()
        {
            if (_incidentTicket.Date != null && _incidentTicket.Type != 0 && !string.IsNullOrWhiteSpace(_incidentTicket.By) && !string.IsNullOrWhiteSpace(_incidentTicket.Priority) && _incidentTicket.Deadline != null && !string.IsNullOrWhiteSpace(_incidentTicket.Subject) && !string.IsNullOrWhiteSpace(_incidentTicket.Description))
                {
                _incidentTicket.SubmitTicket(_incidentTicket);
                shellViewModel.ActivateItem(new IncidentTicketViewModel(shellViewModel));
            }
            else
            {
                MessageBox.Show("Please complete the entire form");
            }
            
        }

        public void CancelTicket()
        {
            shellViewModel.ActivateItem(new IncidentTicketViewModel(shellViewModel));
        }
    }
}
