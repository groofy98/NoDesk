using NoDesk.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NoDesk.Views {
    /// <summary>
    /// Interaction logic for IncidentView.xaml
    /// </summary>
    public partial class IncidentTicketView : UserControl {
        public IncidentTicketView() {
            InitializeComponent();
        }

        private void Filter_GotFocus(object sender, RoutedEventArgs e) {
            if (Filter.Text == "Filter by subject") {
                Filter.Text = "";
            }
        }

        private void Filter_LostFocus(object sender, RoutedEventArgs e) {
            if (Filter.Text == "") {
                Filter.Text = "Filter by subject";
            }
        }

        private void IncidentTickets_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            ShowIncidentTicket.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        }
    }
}
