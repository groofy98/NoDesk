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

namespace NoDesk.Views
{
    /// <summary>
    /// Interaction logic for AddIncidentTicket.xaml
    /// </summary>
    public partial class AddIncidentTicket : UserControl
    {
        List<ComboBox> comboBoxList = new List<ComboBox>();
        public AddIncidentTicket()
        {
            InitializeComponent();

            comboBoxList = AddComboBoxes();
            
        }

        public List<ComboBox> AddComboBoxes()
        {
            
            comboBoxList.Add(IncidentDate);
            comboBoxList.Add(IncidentDeadline);
            comboBoxList.Add(IncidentPriority);
            comboBoxList.Add(IncidentType);

            return comboBoxList;
        }

        private void SubmitTicket_Click(object sender, RoutedEventArgs e)
        {
            foreach(ComboBox comboBox in comboBoxList)
            {

            }
        }
    }
}
