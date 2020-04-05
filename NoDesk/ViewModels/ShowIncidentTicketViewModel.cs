using Caliburn.Micro;
using NoDesk.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoDesk.ViewModels {
    class ShowIncidentTicketViewModel : Screen {
        private ShellViewModel shellViewModel;
        private IncidentTicket _incidentTicket;
        private BindableCollection<IncidentTicket> incidentTickets;

        public IncidentTicket IncidentTicket {
            get { return _incidentTicket; }
            set {
                _incidentTicket = value;
                NotifyOfPropertyChange(() => IncidentTicket);
            }
        }

        public string PossibleSolution {
            get { return FindPossibleSolution(); }
        }

        public ShowIncidentTicketViewModel(ShellViewModel shellViewModel, IncidentTicket incidentTicket, BindableCollection<IncidentTicket> incidentTickets) {
            this.shellViewModel = shellViewModel;
            IncidentTicket = incidentTicket;
            this.incidentTickets = incidentTickets;
        }

        public void Back() {
            shellViewModel.ActivateItem(new IncidentTicketViewModel(shellViewModel));
        }

        public void Save() {
            TicketDal ticketDal = new TicketDal();

            if (IncidentTicket.Solution == null || IncidentTicket.Solution == "") {
                IncidentTicket.Status = false;
            } else {
                IncidentTicket.Status = true;
            }

            ticketDal.UpdateTicket(IncidentTicket);

            Back();
        }
        private string FindPossibleSolution() {
            string possibleSolution = "";

            if (IncidentTicket.Solution != null || IncidentTicket.Solution == "") {
                return possibleSolution;
            }

            int mostSameWords = 0;
            IncidentTicket bestSolution = new IncidentTicket();
            bool bestSolutionChanged = false;

            foreach (IncidentTicket incidentTicket in incidentTickets) {
                if (incidentTicket.Solution == null || incidentTicket.Solution == "" || IncidentTicket.Equals(incidentTicket)) {
                    continue;
                }
                if (IncidentTicket.Type == incidentTicket.Type) {
                    if (IncidentTicket.Subject.ToUpper() == incidentTicket.Subject.ToUpper() || IncidentTicket.Subject.ToUpper().Contains(incidentTicket.Subject.ToUpper()) || incidentTicket.Subject.ToUpper().Contains(IncidentTicket.Subject.ToUpper())) {
                        int sameWords = 0;

                        if (IncidentTicket.Description != null && IncidentTicket.Description != "" && incidentTicket.Description != null && incidentTicket.Description != "") {
                            string[] descriptionWords = incidentTicket.Description.Split(' ');

                            foreach (string word in descriptionWords) {
                                if (IncidentTicket.Description.ToUpper().Contains(word.ToUpper())) {
                                    sameWords++;
                                }
                            }
                        }

                        if (!bestSolutionChanged && mostSameWords == sameWords) {
                            bestSolution = incidentTicket;
                            bestSolutionChanged = true;
                        } else if (mostSameWords < sameWords) {
                            bestSolution = incidentTicket;
                            bestSolutionChanged = true;
                        }
                    }
                }
            }

            if (bestSolutionChanged) {
                return bestSolution.Solution;
            } else {
                return possibleSolution;
            }
        }

    }
}
