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

        public string NewTickets 
        { 
            get 
            {
                TicketDal ticketDal = new TicketDal();
                return ticketDal.GetNewTicketAmount(shellViewModel.LoggedUser).ToString();               
            }                        
        }

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

        private ObservableValue _ticketsPastDeadline;

        public ObservableValue TicketsPastDeadline
        {
            get { return _ticketsPastDeadline; }
            set { _ticketsPastDeadline = value; }
        }


        public string UnresolvedLabel {
            get { return _ticketsOpen.Value + "/" + _ticketsTotal.Value; }
            }

        public string DeadlineLabel 
        {
            get { return "" + _ticketsPastDeadline.Value; }
        }

        public void ShowList()
        {
            this.shellViewModel.ShowTickets();            
        }

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

        public DashboardViewModel(ShellViewModel shellViewModel) {
            this.shellViewModel = shellViewModel;
            TicketDal ticketDal = new TicketDal();

            TicketsPastDeadline = new ObservableValue(ticketDal.GetTicketPastDeadlineAmount(shellViewModel.LoggedUser));
            TicketsPastDeadline.PropertyChanged += (obj, args) =>
            { NotifyOfPropertyChange(() => DeadlineLabel); };

            TicketsOpen = new ObservableValue(ticketDal.GetOpenTicketAmount(shellViewModel.LoggedUser));
            TicketsOpen.PropertyChanged += (obj, args) =>
            { NotifyOfPropertyChange(() => UnresolvedLabel); };

            TicketsTotal = new ObservableValue(ticketDal.GetTotalTicketAmount(shellViewModel.LoggedUser));
            TicketsTotal.PropertyChanged += (obj, args) =>
            { NotifyOfPropertyChange(() => UnresolvedLabel); };

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
