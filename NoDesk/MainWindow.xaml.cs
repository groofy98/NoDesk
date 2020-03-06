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
using MongoDB.Bson;
using MongoDB.Driver;

namespace NoDesk
{
    //"mongodb+srv://Sjors:*FDmqwry+r3rT+i@cluster0-dg3ym.mongodb.net/test?retryWrites=true&w=majority"
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {

            var dbClient = new MongoClient("mongodb+srv://Sjors:Yolo1@cluster0-dg3ym.mongodb.net/test?retryWrites=true&w=majority");
            var dbList = dbClient.ListDatabases().ToList();
            var database = dbClient.GetDatabase("sample_training");
            var collection = database.GetCollection<BsonDocument>("grades");
            var firstDocument = collection.Find(new BsonDocument());
            Console.WriteLine(firstDocument.ToString());
            Console.WriteLine("The list of databases are:");

            foreach (var item in dbList)
            {
                Console.WriteLine(item);
            }
            database = dbClient.GetDatabase("nodesk");
            var collection2 = database.GetCollection<User>("users");
            User user = new User { FirstName = "Twan", LastName = "Grooff", Location = "Haarlem", MailAdress = "twan@hotmail.com", PhoneNumber = 0658848228, Type = UserType.Admin };
            collection2.InsertOne(user);
            var filter = Builders<User>.Filter.Empty;
            List<User> result = collection2.Find(filter).ToList();
            foreach (User u in result)
            {
                Console.WriteLine("name: " + u.FirstName);
                Console.WriteLine(value: u.PrintOutUser());
            }
        }
    }
}
