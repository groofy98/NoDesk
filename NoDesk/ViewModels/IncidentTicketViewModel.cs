using Caliburn.Micro;
using NoDesk.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoDesk.ViewModels {
    class IncidentTicketViewModel {
        private BindableCollection<IncidentTicket> _incidentTickets;
        private ShellViewModel shellViewModel;

        public BindableCollection<IncidentTicket> IncidentTickets {
            get { return _incidentTickets; }
            set { _incidentTickets = value; }
        }

        public IncidentTicketViewModel(ShellViewModel shellViewModel) {
            //IncidentTickets = new BindableCollection<IncidentTicket>(new TicketDal().GetTickets());
            this.shellViewModel = shellViewModel;
        }

        public void CreateIncident() {

        }
    }
}
