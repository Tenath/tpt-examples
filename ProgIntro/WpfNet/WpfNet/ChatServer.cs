using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WpfNet
{
    public class ChatServer : IDisposable
    {
        private MainWindow window;

        private string bindPoint;
        private ushort bindPort;

        //private Socket listenSocket;
        //private List<TcpClient> connections = new List<TcpClient>();
        //private TcpListener listener;
        private UdpClient client;
        private Task<UdpReceiveResult> task;
        //private Thread process_thread;

        public ChatServer(MainWindow win, string iface, ushort port)
        {
            //Socket
            window = win;
            bindPoint = iface;
            bindPort = port;
            //Socket s = new Socket();
            //s.Bind()

            //IPEndPoint endpt = new IPEndPoint(IPAddress.Parse(iface), port);
            //client = new UdpClient(endpt);
            client = new UdpClient(port);
        }

        public void Update()
        {
            // Accept new connections
            /*if (listener.Pending())
            {
                TcpClient cl = listener.AcceptTcpClient();
                if (cl != null) connections.Add(cl);
            }

            foreach (TcpClient c in connections)
            {
                StreamReader rdr = new StreamReader(c.GetStream());
                string line;
                while ((line = rdr.ReadLine()) != null)
                {
                    window.WriteServer(line);
                }
            }*/
            if(task==null)
            {
                task = client.ReceiveAsync();
            }
            else if (task.Status == TaskStatus.RanToCompletion)
            {
                IPEndPoint src = task.Result.RemoteEndPoint;
                string buf = Encoding.UTF8.GetString(task.Result.Buffer);
                string[] lines = buf.Split('\n');
                foreach (string line in lines) window.WriteServer(src.Address.ToString(), line);

                task.Dispose();
                task = null;
            }
        }

        public static List<string> GetLocalIPv4()
        {
            List<string> ipAddrList = new List<string>();
            foreach (NetworkInterface item in NetworkInterface.GetAllNetworkInterfaces())
            {
                if ((item.NetworkInterfaceType == NetworkInterfaceType.Ethernet || item.NetworkInterfaceType == NetworkInterfaceType.Wireless80211) && item.OperationalStatus == OperationalStatus.Up)
                {
                    foreach (UnicastIPAddressInformation ip in item.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            ipAddrList.Add(ip.Address.ToString());
                        }
                    }
                }
            }

            if (!ipAddrList.Contains("127.0.0.1")) ipAddrList.Insert(0, "127.0.0.1");
            return ipAddrList;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                //listenSocket.Shutdown(SocketShutdown.Both);
                //listenSocket.Close();
                client.Close();
                if (task != null) task.Dispose();
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~ChatServer() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
