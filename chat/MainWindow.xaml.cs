using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
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

namespace chat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void CreateNew_Click(object sender, RoutedEventArgs e)
        {
            if (Username.Text == null || Username.Text == string.Empty)
            {
                MessageBox.Show("Вы не ввели никнейм!");
                return;
            }
            adminServer Create = new adminServer(Username.Text);
            Create.Show();
            this.Close();
        }

        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            if (Username.Text == null || Username.Text == string.Empty || Ip_adress.Text == null || Ip_adress.Text == string.Empty)
            {
                MessageBox.Show("Вы не ввели никнейм или ip-адресс!");
                return;
            }
            user UserConnectedChat = new user(Username.Text, Ip_adress.Text);
            UserConnectedChat.Show();
            this.Close();
        }
    }
}

