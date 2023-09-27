using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;

namespace chat
{
    /// <summary>
    /// Interaction logic for user.xaml
    /// </summary>
    public partial class user : Window
    {
        private Socket server;

        public user(string username, string ipadress)
        {
            InitializeComponent();
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Any, 8888);
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            server.ConnectAsync(ipadress, 8888);
            RecieveMessege();
        }
        private async Task RecieveMessege()
        {
            while (true)
            {
                byte[] bytes = new byte[1024];
                await server.ReceiveAsync(bytes, SocketFlags.None);
                string message = Encoding.UTF8.GetString(bytes);
                MessageListBox.Items.Add(message);
            }
        }
        private async Task SendMessage(string msg)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(msg);
            await server.SendAsync(bytes, SocketFlags.None);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SendMessage(txb.Text);
            if (txb.Text.Contains("/disconnect"))
            {
                this.Close();
            }
            
        }

        private void Button_Click_Out(object sender, RoutedEventArgs e)
        {
            this.Close();
            
        }
        private void close(object sender, CancelEventArgs e)
        {
            MainWindow MW = new MainWindow();
            MW.Show();
        }
    }
}
