using Caliburn.Micro;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using NoDesk.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoDesk.ViewModels
{
    public class DashboardViewModel : Screen
    {
        ShellViewModel shellViewModel;

        private ObservableValue _ticketsSolved;

        public ObservableValue TicketsSolved
        {
            get { return new ObservableValue(_ticketsTotal.Value - _ticketsOpen.Value); }
            
        }


        private ObservableValue _ticketsTotal;        

        public ObservableValue TicketsTotal
        {
            get { return _ticketsTotal; }
            set { _ticketsTotal = value;
                NotifyOfPropertyChange(() => UnresolvedLabel);
                NotifyOfPropertyChange(() => TicketsTotal);
            }
        }

        private ObservableValue _ticketsOpen;

        public ObservableValue TicketsOpen
        {
            get { return _ticketsOpen; }
            set { _ticketsOpen = value;                
                NotifyOfPropertyChange(() => UnresolvedLabel);
                NotifyOfPropertyChange(() => TicketsOpen);
                NotifyOfPropertyChange(() => TicketsSolved);
            }
        }

        public string UnresolvedLabel {
            get { return _ticketsOpen.Value + "/" + _ticketsTotal.Value; }
            }        

        public void ShowList()
        {
            this.shellViewModel.ShowTickets();            
        }

        public SeriesCollection SeriesCollection { get; set; }

        public DashboardViewModel(ShellViewModel shellViewModel) {
            this.shellViewModel = shellViewModel;
            TicketDal ticketDal = new TicketDal();
            Console.WriteLine("Amount of tickets: " + ticketDal.GetTotalTicketAmount());
            Console.WriteLine("Tickets past deadline: " + ticketDal.GetTicketPastDeadline());

            TicketsOpen = new ObservableValue(ticketDal.GetOpenTicketAmount());
            TicketsOpen.PropertyChanged += (obj, args) =>
            { NotifyOfPropertyChange(() => UnresolvedLabel); };

            TicketsTotal = new ObservableValue(ticketDal.GetTotalTicketAmount());
            TicketsTotal.PropertyChanged += (obj, args) =>
            { NotifyOfPropertyChange(() => UnresolvedLabel); };            

            SeriesCollection = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "open",
                    Values = new ChartValues<ObservableValue> {TicketsOpen},
                    DataLabels = false,
                    Fill = System.Windows.Media.Brushes.Orange
                },
                new PieSeries
                {
                    Title = "solved",
                    Values = new ChartValues<ObservableValue> { TicketsSolved },
                    DataLabels = false,
                    Fill = System.Windows.Media.Brushes.DarkSlateGray
                }
            };
        }



    }
}
