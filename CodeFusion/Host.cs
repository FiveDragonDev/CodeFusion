using System.Net;
using System.Net.Sockets;

namespace CodeFusion
{
    public class Host
    {
        public event Action<TcpClient>? OnClientConnect;

        private readonly TcpListener _listener;

        public Host(int port)
        {
            _listener = new TcpListener(IPAddress.Any, port);
        }

        public void Start()
        {
            _listener.Start();
            Console.WriteLine($"Server started on port " +
                $"{((IPEndPoint)_listener.LocalEndpoint).Port}");
        }
        public void Stop()
        {
            _listener.Stop();
            Console.WriteLine("Server stopped");
        }
        public void Listen()
        {
            while ( true )
            {
                TcpClient client = _listener.AcceptTcpClient();
                Console.WriteLine($"Client connected from " +
                    $"{(client.Client.RemoteEndPoint as IPEndPoint)?.Address}");

                if (client != null)
                    OnClientConnect?.Invoke(client);
            }
        }
        public static void Connect(string hostname, int port)
        {
            using TcpClient client = new();
            client.Connect(hostname, port);
            Console.WriteLine("Connected to server at " + hostname + ":" + port);

            client.Close();
            Console.WriteLine("Disconnected from server");
        }

    }

}
