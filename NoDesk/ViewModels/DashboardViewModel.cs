using Caliburn.Micro;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
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
            }
        }

        public string UnresolvedLabel {
            get { return _ticketsOpen.Value + "/" + _ticketsTotal.Value; }
            }        

        public void ShowList()
        {
                         
            TicketsTotal.Value = TicketsTotal.Value + 1;            
        }

        public SeriesCollection SeriesCollection { get; set; }

        public DashboardViewModel(ShellViewModel shellViewModel) {
            this.shellViewModel = shellViewModel;

            TicketsOpen = new ObservableValue(7);
            TicketsOpen.PropertyChanged += (obj, args) =>
            { NotifyOfPropertyChange(() => UnresolvedLabel); };

            TicketsTotal = new ObservableValue(15);
            TicketsTotal.PropertyChanged += (obj, args) =>
            { NotifyOfPropertyChange(() => UnresolvedLabel); };

            SeriesCollection = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "total",
                    Values = new ChartValues<ObservableValue> {TicketsTotal},
                    DataLabels = false
                },
                new PieSeries
                {
                    Title = "open",
                    Values = new ChartValues<ObservableValue> { TicketsOpen },
                    DataLabels = false
                }
            };
        }



    }
}
