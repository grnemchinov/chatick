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
    /// Interaction logic for adminServer.xaml
    /// </summary>
    public partial class adminServer : Window
    {
        private Socket socket;
        private List<Socket> clients = new List<Socket>();
        public adminServer(string username)
        {
            InitializeComponent();
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Any, 8888);
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(ipPoint);
            socket.Listen(1000);
            ListenToClients();
        }
        private async Task ListenToClients()
        {
            while (true)
            {
                var client = await socket.AcceptAsync();
                clients.Add(client);
                RecieveMessage(client);
            }
        }
        private async Task RecieveMessage(Socket client)
        {
            while (true)
            {
                byte[] bytes = new byte[1024];
                await client.ReceiveAsync(bytes, SocketFlags.None);
                string message = Encoding.UTF8.GetString(bytes);

                MessageListBox.Items.Add($"[Сообщение от {client.RemoteEndPoint}]: {message}");

                foreach (var item in clients)
                {
                    SendMessage(item, message);
                }
            }
        }
        private async Task SendMessage(Socket client, string message)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(message);
            await client.SendAsync(bytes, SocketFlags.None);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (txb.Text.Contains("/disconnect"))
            {
                this.Close();
            }
            else
            {
                var message = txb.Text;
                foreach (var item in clients)
                {
                    SendMessage(item, message);
                }
                MessageListBox.Items.Add(message);
            }
            
        }
        private void close(object sender, CancelEventArgs e)
        {
            MainWindow MW = new MainWindow();
            MW.Show();
        }
    }
}
