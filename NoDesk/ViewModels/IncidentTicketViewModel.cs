using Caliburn.Micro;
using NoDesk.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoDesk.ViewModels {
    class IncidentTicketViewModel : Screen {
        private BindableCollection<IncidentTicket> _incidentTickets;
        private ShellViewModel shellViewModel;
        private bool _selection;

        private bool Selection {
            get { return _selection; }
            set {
                _selection = value;
                NotifyOfPropertyChange(() => CanEditIncidentTicket);
                NotifyOfPropertyChange(() => CanDeleteIncidentTicket);
            }
        }

        public BindableCollection<IncidentTicket> IncidentTickets {
            get { return _incidentTickets; }
            set { _incidentTickets = value; }
        }

        public IncidentTicketViewModel(ShellViewModel shellViewModel) {
            IncidentTickets = new BindableCollection<IncidentTicket>(new TicketDal().GetTickets());
            Selection = false;
            this.shellViewModel = shellViewModel;
        }

        public void CreateIncident() {
            shellViewModel.ActivateItem(new AddIncidentTicketViewModel(shellViewModel));
        }

        public void EditIncidentTicket() {
            Console.WriteLine("edit");
            Console.WriteLine(Selection);
        }

        public void DeleteIncidentTicket() {
            Console.WriteLine("remove");
            Console.WriteLine(Selection);
        }

        public bool CanEditIncidentTicket {
            get {
                return Selection;
            }
        }

        public bool CanDeleteIncidentTicket {
            get {
                return Selection;
            }
        }
    }
}
