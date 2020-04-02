﻿using Caliburn.Micro;
using NoDesk.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NoDesk.ViewModels {
    class IncidentTicketViewModel : Screen {
        private BindableCollection<IncidentTicket> _incidentTickets;
        private IncidentTicket _selectedIncidentTicket;
        private ShellViewModel shellViewModel;
        private string _filter = "Filter by subject";
        private List<IncidentTicket> hidedIncidentTickets;

        public IncidentTicket SelectedIncidentTicket {
            get { return _selectedIncidentTicket; }
            set {
                _selectedIncidentTicket = value;
                NotifyOfPropertyChange(() => SelectedIncidentTicket);
                NotifyOfPropertyChange(() => CanSaveIncidentTicket);
                NotifyOfPropertyChange(() => CanDeleteIncidentTicket);
            }
        }

        public BindableCollection<IncidentTicket> IncidentTickets {
            get { return _incidentTickets; }
            set { _incidentTickets = value;
                NotifyOfPropertyChange(() => IncidentTickets);
            }
        }

        public string Filter {
            get { return _filter; }
            set {
                _filter = value;
                FilterIncidentTickets();
            }
        }

        public IncidentTicketViewModel(ShellViewModel shellViewModel) {
            this.shellViewModel = shellViewModel;

            IncidentTickets = new BindableCollection<IncidentTicket>(new TicketDal().GetTickets());
            hidedIncidentTickets = new List<IncidentTicket>();
        }

        public void CreateIncident() {
            shellViewModel.ActivateItem(new AddIncidentTicketViewModel(shellViewModel));
        }

        public void SaveIncidentTicket() {
            TicketDal ticketDal = new TicketDal();

            foreach (IncidentTicket incidentTicket in IncidentTickets) {
                ticketDal.UpdateTicket(incidentTicket);
            }

            IncidentTickets = new BindableCollection<IncidentTicket>(ticketDal.GetTickets());
            SelectedIncidentTicket = null;
        }

        public void DeleteIncidentTicket() {
            if (MessageBox.Show("Are you sure you want to delete a incident?",
                "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes) {
                TicketDal ticketDal = new TicketDal();

                ticketDal.DeleteTicket(SelectedIncidentTicket);

                IncidentTickets = new BindableCollection<IncidentTicket>(ticketDal.GetTickets());
            }

            SelectedIncidentTicket = null;
        }

        public bool CanSaveIncidentTicket {
            get { return !(SelectedIncidentTicket == null); }
        }

        public bool CanDeleteIncidentTicket {
            get { return !(SelectedIncidentTicket == null); }
        }

        public void FilterIncidentTickets() {
            if (Filter == "Filter by subject") {
                return;
            }

            if (Filter == "") {
                foreach (IncidentTicket incidentTicket in hidedIncidentTickets) {
                    IncidentTickets.Add(incidentTicket);
                }

                hidedIncidentTickets = new List<IncidentTicket>();
                return;
            }

            List<IncidentTicket> removedIncidentTickets = new List<IncidentTicket>();

            if (hidedIncidentTickets.Count > 0) {
                foreach (IncidentTicket incidentTicket in hidedIncidentTickets) {
                    if (incidentTicket.Subject.Contains(Filter)) {
                        IncidentTickets.Add(incidentTicket);
                        removedIncidentTickets.Add(incidentTicket);
                    }
                }

                if (removedIncidentTickets.Count > 0) {
                    foreach (IncidentTicket incidentTicket in removedIncidentTickets) {
                        hidedIncidentTickets.Remove(incidentTicket);
                    }
                }

                removedIncidentTickets = new List<IncidentTicket>();
            }

            foreach (IncidentTicket incidentTicket in IncidentTickets) {
                if (!incidentTicket.Subject.Contains(Filter)) {
                    hidedIncidentTickets.Add(incidentTicket);
                    removedIncidentTickets.Add(incidentTicket);
                }
            }

            if (removedIncidentTickets.Count > 0) {
                foreach (IncidentTicket incidentTicket in removedIncidentTickets) {
                    IncidentTickets.Remove(incidentTicket);
                }
            }
        }
    }
}
