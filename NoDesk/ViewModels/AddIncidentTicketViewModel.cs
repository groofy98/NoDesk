using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoDesk.ViewModels
{
    class AddIncidentTicketViewModel
    {
        ShellViewModel shellViewModel;
        IncidentTicket _incidentTicket;

        public AddIncidentTicketViewModel(ShellViewModel shellViewModel)
        {
            this.shellViewModel = shellViewModel;
            _incidentTicket = new IncidentTicket();
        }

        public DateTime IncidentDate
        {
            set { _incidentTicket.Date = value; }
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

        private string Deadline;

        public string IncidentDeadline
        {
            set { _incidentTicket.Deadline = value;  }
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
