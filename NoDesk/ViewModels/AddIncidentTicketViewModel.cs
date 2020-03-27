using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoDesk.ViewModels
    {
        class AddIncidentTicketModel
        {
            ShellViewModel shellViewModel;
            public AddIncidentTicketModel(ShellViewModel shellViewModel)
            {
                this.shellViewModel = shellViewModel;
            }

            public IncidentTicket incidentTicket;
            public IncidentTicket IncidentTicket
            {
                get { return incidentTicket; }
                set { incidentTicket = value; }
            }

            public bool CanSubmitTicket()
            {
                return (incidentTicket.Date == null || incidentTicket.Type == 0 || incidentTicket.By == null || incidentTicket.Priority == 0 || incidentTicket.Deadline == 0 || incidentTicket.Subject == null || incidentTicket.Description == null);
            }

            public void SubmitButton()
            {

            }
        }
    }


