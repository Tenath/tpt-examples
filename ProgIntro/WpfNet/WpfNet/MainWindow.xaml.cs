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
using System.Windows.Threading;

namespace WpfNet
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<string> LocalIPs;

        ChatServer server = null;
        ChatClient client = null;

        DispatcherTimer timer = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LocalIPs = ChatServer.GetLocalIPv4().ToList();

            cboNetIf.DataContext = this;
            cboNetIf.ItemsSource = LocalIPs;

            if (LocalIPs.Count > 0) cboNetIf.SelectedIndex = 0;

            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            timer.Start();
        }

        private void BtnClientConnect_Click(object sender, RoutedEventArgs e)
        {
            // Проверка корректности входных данных
            if(tbHost.Text.Length == 0)
            {
                MessageBox.Show("Не введён адрес для подключения!");
                return;
            }

            if(!ushort.TryParse(tbPort.Text, out ushort port) || port==0)
            {
                MessageBox.Show("Неверно указан порт! Допустимый диапазон: 1-65535");
                return;
            }

            if (tbMessage.Text.Length == 0)
            {
                MessageBox.Show("Пустое сообщение!");
                return;
            }

            try
            {
                //IPEndPoint ep = new IPEndPoint(IPAddress.Parse(tbHost.Text), port);
                //IPEndPoint lep = new IPEndPoint(IPAddress.Parse("172.16.2.24"), 50000);
                using (UdpClient client = new UdpClient(tbHost.Text, port))
                {
                    byte[] data = Encoding.UTF8.GetBytes(tbMessage.Text);
                    
                    int sent = client.Send(data, data.Length);
                }
            }
            catch(Exception exc)
            {
                MessageBox.Show($"Исключение: {exc.Message}\n{exc.StackTrace}");
            }
            
        }

        public void timer_Tick(object sender, EventArgs e)
        {
            if(server!=null) server.Update();
        }

        public void WriteServer(string src, string s)
        {
            DateTime now = DateTime.Now;
            tbOutput.AppendText($"[{now.Hour:D2}:{now.Minute:D2}:{now.Second:D2}] ({src}) {s}\n");
        }

        private void BtnServe_Click(object sender, RoutedEventArgs e)
        {
            if(server!=null)
            {
                server.Dispose();
                server = null;
                btnServe.Content = "Запустить";
                lbServerConnStatus.Content = "Выключен";
                return;
            }

            if(LocalIPs.Count==0)
            {
                MessageBox.Show("Отсутствует адрес для привязки!");
            }

            if (!ushort.TryParse(tbServerPort.Text, out ushort port) || port == 0)
            {
                MessageBox.Show("Неверно указан порт! Допустимый диапазон: 1-65535");
                return;
            }

            try
            {
                server = new ChatServer(this, cboNetIf.SelectedItem as string,port);

                btnServe.Content = "Остановить";
                lbServerConnStatus.Content = "Запущен";
            }
            catch(Exception exc)
            {
                MessageBox.Show($"Исключение: {exc.Message}");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            tbOutput.Text = "";
        }
    }
}
