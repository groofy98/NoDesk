using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using NoDesk.Dal;
using System.Net.Mail;

namespace NoDesk.ViewModels
{
    class AddUserViewModel : Screen
    {
        public ShellViewModel shellViewModel;
        User user = new User();
        UserDal userDal = new UserDal();

        public string usernameInput { get; set; }
        public string emailInput { get; set; }

        public AddUserViewModel(ShellViewModel shellViewModel)
        {
            this.shellViewModel = shellViewModel;
        }

        //Setters for user values
        public string FirstName
        {
            set { user.FirstName = value; }
        }

        public string LastName
        {
            set { user.LastName = value; }
        }

        public string Username
        {
            set { user.Username = value; }
        }

        public string Password
        {
            set { user.Password = value; }
        }
        public UserType Type
        {
            set { user.Type = value; }
        }

        public string Mail
        {
            set { user.MailAddress = value; }
        }

        public int PhoneNumber
        {
            set { user.PhoneNumber = value; }
        }

        public string Location
        {
            set { user.Location = value; }
        }

        //--------------

        public void CancelButton()
        {
            //Returns back to userview
            shellViewModel.ActivateItem(new UserViewModel(shellViewModel));

        }
        public void AddUser()
        {
            List<User> users = userDal.GetUserByUsername(user.Username);
            List<User> emails = userDal.GetUserbyEmail(user.MailAddress);

            //Checks if username already exists
            if (users.Count > 0 && users[0].Username == user.Username)
            {
                MessageBox.Show("User already exists");
            }
            //Checks if email already exists
            else if (emails.Count > 0 && emails[0].MailAddress == user.MailAddress)
            {
                MessageBox.Show("Email address already exists");
            }

            //Adds user to database
            else
            {
                //Generates password and changes user's password
                string password = GeneratePassword(10);
                user.Password = password;
                userDal.UpdateUser(user);

                //Sends email to user with new generated password
                string message = "Dear " + user.FirstName + ", Your new password is " + password;
                SendEmail(user, message);

                //Adds user and gives confirmation message
                user.AddUser(user);
                MessageBox.Show("User added!, Check your email for the password");
                shellViewModel.ActivateItem(new UserViewModel(shellViewModel));
            }


        }

        private string GeneratePassword(int length)
        {
            //Generates password
            const string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789!@$?_-";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(allowedChars[rnd.Next(allowedChars.Length)]);
            }
            return res.ToString();
        }


        private void SendEmail(User user, string message)
        {
            //Sends email
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

            try
            {
                client.Send(mail);
            }
            catch
            {
                MessageBox.Show("Problem with mailing server. Please contact our servicedesk.");
            }
        }
    }
}

