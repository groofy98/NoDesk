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
        private readonly ShellViewModel shellViewModel;


        //Populates a label with the amount of tickets added since last login
        public string NewTickets 
        { 
            get 
            {
                TicketDal ticketDal = new TicketDal();
                return ticketDal.GetNewTicketAmount(shellViewModel.LoggedUser).ToString();               
            }                        
        }
        // The value needed to populate the dashboard 
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

        // Value to fill Gauge on dashboard
        public ObservableValue TicketsOpen
        {
            get { return _ticketsOpen; }
            set { _ticketsOpen = value;                
                NotifyOfPropertyChange(() => UnresolvedLabel); //Updates the label if the amount of open tickets is changed
                NotifyOfPropertyChange(() => TicketsOpen);
                NotifyOfPropertyChange(() => TicketsSolved);
            }
        }

        private ObservableValue _ticketsPastDeadline;

        public ObservableValue TicketsPastDeadline
        {
            get { return _ticketsPastDeadline; }
            set { _ticketsPastDeadline = value; }
        }

        // Contructs a label for the first gauge
        public string UnresolvedLabel {
            get { return _ticketsOpen.Value + "/" + _ticketsTotal.Value; }
            }


        public string DeadlineLabel 
        {
            get { return "" + _ticketsPastDeadline.Value; }
        }
        // Changes from dashboard to ticket view
        public void ShowList()
        {
            this.shellViewModel.ShowTickets();            
        }

        // Loads up a ticketview but only the tickets past deadline
        public void ShowTicketsPastDeadline()
        {            
            var incidents = new IncidentTicketViewModel(this.shellViewModel)
            {
                IncidentTickets = new BindableCollection<IncidentTicket>(new TicketDal().GetTicketsPastDeadline(shellViewModel.LoggedUser))
            };
            this.shellViewModel.ActivateItem(incidents);
        }

        
        public SeriesCollection SeriesCollection { get; set; }

        public SeriesCollection DeadlineData { get; set; }

        
        // Constructor
        public DashboardViewModel(ShellViewModel shellViewModel) {
            this.shellViewModel = shellViewModel;
            TicketDal ticketDal = new TicketDal();

            // Get values from database and put them in a ObservableValue object
            TicketsPastDeadline = new ObservableValue(ticketDal.GetTicketPastDeadlineAmount(shellViewModel.LoggedUser));
            TicketsPastDeadline.PropertyChanged += (obj, args) =>
            { NotifyOfPropertyChange(() => DeadlineLabel); }; // Adding a listener to the object so the label changes with the value

            TicketsOpen = new ObservableValue(ticketDal.GetOpenTicketAmount(shellViewModel.LoggedUser));
            TicketsOpen.PropertyChanged += (obj, args) =>
            { NotifyOfPropertyChange(() => UnresolvedLabel); };

            TicketsTotal = new ObservableValue(ticketDal.GetTotalTicketAmount(shellViewModel.LoggedUser));
            TicketsTotal.PropertyChanged += (obj, args) =>
            { NotifyOfPropertyChange(() => UnresolvedLabel); };

            // Put the Observable in a collection so they can be bound to the gauges
            DeadlineData = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "Past deadline",
                    Values = new ChartValues<ObservableValue> {TicketsPastDeadline},
                    DataLabels = false,
                    Fill = System.Windows.Media.Brushes.Red
                },
                new PieSeries
                {
                    Title = "Not past deadline",
                    Values = new ChartValues<ObservableValue> { new ObservableValue( TicketsOpen.Value - TicketsPastDeadline.Value )},
                    DataLabels = false,
                    Fill = System.Windows.Media.Brushes.Gray
                }
            };

            
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
                    Fill = System.Windows.Media.Brushes.Gray
                }
            };
        }



    }
}
