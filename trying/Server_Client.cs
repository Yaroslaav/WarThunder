using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.IO;

public class Server_Client
{
    public bool reading = false;

    string _message = "";

    public Action<string> OnRead;

    public bool isServer = true;

    public TcpListener listener;
    public TcpClient client;

    public NetworkStream stream;
    public void StartServer()
    {
        IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
        int port = 12345;

        listener = new TcpListener(ipAddress, port);

        listener.Start();
        Console.WriteLine("The server is started at the address {0}:{1}", ipAddress, port);

        client = listener.AcceptTcpClient();
        Console.WriteLine("The client has connected: {0}", client.Client.RemoteEndPoint);

        stream = client.GetStream();
    }
    public void StopServer()
    {
        client.Close();
        listener.Stop();
    }

    public void StartClient()
    {
        IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
        int port = 12345;

        client = new TcpClient();

        client.Connect(ipAddress, port);

        stream = client.GetStream();

    }
    public void StopClient() => client.Close();

    public async void ClientRead()
    {
            await Task.Run(() =>
            {
                reading = true;
                byte[] data = new byte[1024];
                int bytes = stream.Read(data, 0, data.Length);
                _message = Encoding.UTF8.GetString(data, 0, bytes );
                OnRead?.Invoke(_message);
                reading = false;
            });
    }
    public async void ServerRead()
    {
        await Task.Run(() =>
        {
            reading = true;
            byte[] _data = new byte[1024];
            int _bytesRead = stream.Read(_data, 0, _data.Length);
            _message = Encoding.UTF8.GetString(_data, 0, _bytesRead);

            OnRead?.Invoke(_message);
            reading = false;
        });
    }

    public void SendMessage(string message)
    {
        byte[] data = Encoding.UTF8.GetBytes(message);
        stream.Write(data, 0, data.Length);
    }
    public void ReadMessage()
    {
        if (reading)
            return;
        if(isServer)
        {
            ClientRead();
        }
        else
        {
            ServerRead();
        }
    }
    public void Stop()
    {
        if (isServer)
        {
            StopServer();
        }
        else
        {
            StopClient();
        }
    }
}