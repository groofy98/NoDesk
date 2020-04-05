using Caliburn.Micro;
using NoDesk.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace NoDesk.ViewModels {
    class LoginViewModel : Screen {
        UserDal userDAL = new UserDal();
        public ShellViewModel shellViewModel;

        public Func<string> PasswordHandler { get; set; }
        public string usernameInput { get; set; }
        public string passwordInput { get; set; }

        public LoginViewModel(ShellViewModel shellViewModel) {
            this.shellViewModel = shellViewModel;
        }

        public void LoginButton() {
            List<User> users = userDAL.GetUserByUsername(usernameInput);
            if (users.Count > 0 && users[0].Password == passwordInput) { //user is found and password matches
                this.shellViewModel.LoggedUser = users[0];

                //check if user is employee
                if (shellViewModel.LoggedUser.Type == UserType.Employee) {
                    string verificationCode = VerifyUser();
                    this.shellViewModel.ActivateItem(new LoginVerificationViewModel(this.shellViewModel, verificationCode)); //extra user verification measurement for employees.
                } else {
                    this.shellViewModel.ActivateItem(new IncidentTicketViewModel(this.shellViewModel)); //show tickets after login    
                }                         
            } else {
                MessageBox.Show("Wrong username or password. Please try again.");
            }         
        }

        public void OnPasswordChanged(PasswordBox source) { //caliburn method, when password is changed in pwbox it dynamically updates the passwordInput property
            passwordInput = source.Password;
        }

        public void PasswordResetButton() {
            if (usernameInput != null) {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Reset password for " + usernameInput + "?", "Password Reset", System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes) {
                    ResetPassword();
                }
            } else {
                MessageBox.Show("Please fill in a username.");
            }       
        }

        private void ResetPassword() {
            List<User> users = userDAL.GetUserByUsername(usernameInput);
            string password = GenerateRandomString(10);
            if (users.Count > 0) {
                users[0].Password = password;
                userDAL.UpdateUser(users[0]);

                string message = "Your password has been reset. You can now log in with the following password: " + password;
                SendEmail(users[0], message);               
            }
            MessageBox.Show("If there is a user with this username an email with your new password has been sent to the corresponding mail address.");
        }

        private string VerifyUser() {
            List<User> users = userDAL.GetUserByUsername(usernameInput);
            string verificationCode = GenerateRandomString(3); //low number for demonstrative purposes
            if (users.Count > 0) {           
                string message = "Please log in to NoDesk using the following code: " + verificationCode;
                SendEmail(users[0], message);

                //FOR DEMONSTRATIVE PURPOSES; so you don't have to check the email for the code. Will be removed in final product.
                MessageBox.Show("Code for Demonstration: \n" + verificationCode); 
            }
            return verificationCode;     
        }

        private string GenerateRandomString(int length){
            string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789!@$?_-";
            char[] chars = new char[length];
            Random rd = new Random();

            for (int i = 0; i < length; i++) {
                chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
            }

            return new string(chars);
        }

        private void SendEmail(User user, string message) {
            MailMessage mail = new MailMessage();
            mail.Body = message;
            mail.To.Add(new MailAddress(user.MailAddress));
            mail.From = new MailAddress("NoDesk2020@gmail.com");
            System.Net.NetworkCredential auth = new System.Net.NetworkCredential("NoDesk2020@gmail.com", "_PyoN@57rQ");
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            mail.IsBodyHtml = true;
            client.Credentials = auth;

            try {
               client.Send(mail);
            } catch {
                MessageBox.Show("Problem with mailing server. Please contact our servicedesk.");
            }
        }

    }
 }
