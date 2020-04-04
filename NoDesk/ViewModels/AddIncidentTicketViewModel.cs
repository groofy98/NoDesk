using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoDesk.Dal;

namespace NoDesk.ViewModels
{
    class AddIncidentTicketViewModel
    {
        ShellViewModel shellViewModel;
        IncidentTicket _incidentTicket;
        BindableCollection<User> _users;
        

        public AddIncidentTicketViewModel(ShellViewModel shellViewModel)
        {
            this.shellViewModel = shellViewModel;
            _incidentTicket = new IncidentTicket();

            _users = new BindableCollection<User>(new UserDal().GetUsers());
        }

        public BindableCollection<User> Users
        {
            get { return _users; }
            set
            {
                _users = value;
            }
        }

        public DateTime IncidentDate
        {
            set { _incidentTicket.Date = value.Date; }
        }

        public string IncidentSubject
        {
            set { _incidentTicket.Subject = value; }
        }

        public string IncidentReportedBy
        {
            set { _incidentTicket.By = value; }
        }

        public string IncidentPriority
        {
            set { _incidentTicket.Priority = value; }
        }

        public DateTime IncidentDeadline
        {
            set { _incidentTicket.Deadline = value.Date;  }
        }

        public IncidentType IncidentType
        {
            set { _incidentTicket.Type = value; }
        }

        public string IncidentDescription
        {
            set { _incidentTicket.Description = value; }
        }

        public void SubmitTicket()
        {
            _incidentTicket.SubmitTicket(_incidentTicket);
        }
    }
}
