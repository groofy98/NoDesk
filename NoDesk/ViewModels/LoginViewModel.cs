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
        public ShellViewModel shellViewModel;
        UserDal userDAL = new UserDal();

        public Func<string> PasswordHandler { get; set; }

        public LoginViewModel(ShellViewModel shellViewModel) {
            this.shellViewModel = shellViewModel;
        }

        public string usernameInput { get; set; }
        public string passwordInput { get; set; }

        public void LoginButton() {
            List<User> users = userDAL.GetUserByUsername(usernameInput);
            if (users.Count > 0 && users[0].Password == passwordInput) { //user is found and password matches
                this.shellViewModel.LoggedUser = users[0];

                if (shellViewModel.LoggedUser.Type == UserType.Admin) {
                    this.shellViewModel.ActivateItem(new DashboardViewModel(this.shellViewModel)); //if employee logs on, show dashboard after login    
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
                MessageBox.Show("Please fill in username or email address.");
            }       
        }

        private void ResetPassword() {
            List<User> users = userDAL.GetUserByUsername(usernameInput);
            string password = GenerateRandomPassword();
            if (users.Count > 0) {
                users[0].Password = password;
                userDAL.UpdateUser(users[0]);

                //composing email with new password
                MailMessage mail = new MailMessage();
                mail.Body = "Your password has been reset. You can now log in with the following password: " + password;
                mail.To.Add(new MailAddress(users[0].MailAddress));
                mail.From = new MailAddress("634293@student.inholland.nl");
                System.Net.NetworkCredential auth = new System.Net.NetworkCredential("mailto:634293@student.inholland.nl%22,%20%22password", "password");
                SmtpClient client = new SmtpClient("student.inholland.nl", 587);
                client.EnableSsl = false;
                client.UseDefaultCredentials = false;
                mail.IsBodyHtml = true;
                client.Credentials = auth;

                try {
                    client.Send(mail); //send email to corresponding mail address with new password.
                } catch {
                    //no mail server set up for this project, so the mail exceptions are just being caught.
                }               
            }
            MessageBox.Show("If there is a user with this username an email has been sent to the corresponding mail address.");
        }

        private string GenerateRandomPassword(){
            string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789!@$?_-";
            char[] chars = new char[10];
            Random rd = new Random();

            for (int i = 0; i < 10; i++) {
                chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
            }

            return new string(chars);
        }

    }
 }
