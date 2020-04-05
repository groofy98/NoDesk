using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NoDesk.ViewModels {
    class LoginVerificationViewModel {
        ShellViewModel shellViewModel;

        public string VerificationCode { get; set; }
        public string CodeInput { get; set; }

        public LoginVerificationViewModel(ShellViewModel shellViewModel, string verificationCode) {
            this.shellViewModel = shellViewModel;
            this.VerificationCode = verificationCode;
        }

        public void LoginButton() {
            if(VerificationCode == CodeInput) {
                this.shellViewModel.ActivateItem(new DashboardViewModel(this.shellViewModel)); //show dashboard
            } else {
                MessageBox.Show("Verification code was not filled in correctly. Please try again or contact our service desk.");
            }   
            
        }
    }
}
