using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoDesk.ViewModels
{
    public class ShellViewModel : Conductor<Object>
    {

		public ShellViewModel()
		{
			ActivateItem(new DashboardViewModel());
		}		

		public void ShowUsers()
		{
			ActivateItem(new UserViewModel());
		}

		public void ShowDashboard()
		{
			ActivateItem(new DashboardViewModel());
		}



	}
}
