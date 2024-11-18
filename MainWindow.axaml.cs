using Avalonia.Controls;
using demo1112remake.Models;
using Metsys.Bson;
using System.Collections.Generic;
using System.Linq;

namespace demo1112remake
{
    public partial class MainWindow : Window
    {
        private List<User> _Users = demo1112remake.Hepler.DBContext.Users.ToList();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Login(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (tbox_login.Text != "" && tbox_password.Text != "")
            {
                User LoggingInUser = _Users.Find(x => x.Login == tbox_login.Text);
                if (LoggingInUser.Password == tbox_password.Text)
                {
                    ListWindow listWindow = new ListWindow(LoggingInUser);
                    listWindow.Show();
                    Close();
                }
            }
        }

        private void Guest(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            ListWindow listWindow = new ListWindow();
            listWindow.Show();
            Close();
        }
    }
}