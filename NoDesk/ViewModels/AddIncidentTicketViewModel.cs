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
        public AddIncidentTicketViewModel(ShellViewModel shellViewModel)
        {
            this.shellViewModel = shellViewModel;
        }

        private DateTime Date;

        public DateTime IncidentDate
        {
            get { return Date; }
            set { Date = value; _incidentTicket.Date = value; }
        }

        private string Subject;

        public string IncidentSubject
        {
            get { return Subject; }
            set { Subject = value; _incidentTicket.Subject = value; }
        }

        private string User;

        public string IncidentReportedBy
        {
            get { return User; }
            set { User = value; _incidentTicket.By = value; }
        }

        private string Priority;

        public string IncidentPriority
        {
            get { return Priority; }
            set { Priority = value; _incidentTicket.Priority = value; }
        }

        private string Deadline;

        public string IncidentDeadline
        {
            get { return Deadline; }
            set { Deadline = value; _incidentTicket.Deadline = value;  }
        }

        private IncidentType Type;

        public IncidentType IncidentType
        {
            get { return Type; }
            set {
                Type = value;
                _incidentTicket.Type = value;
            }
        }

        private string Description;

        public string IncidentDescription
        {
            get { return Description; }
            set { Description = value; _incidentTicket.Description = value; }
        }

        private IncidentTicket _incidentTicket = new IncidentTicket();

        public bool CanSubmitTicket(DateTime date, IncidentType type, string user, int priority, string deadline, string subject, string description)
        {
            return (date == null || type == 0|| user == null || priority == 0 || deadline == null || subject == null || description == null) ;
        }

        public void SubmitTicket()
        {
            _incidentTicket.SubmitTicket(_incidentTicket);
        }
    }
}
