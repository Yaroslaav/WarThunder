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
        /*
        ServerRead();
        //Console.WriteLine("Клієнт надіслав повідомлення: {0}", message);

        byte[] response = Encoding.UTF8.GetBytes("Повідомлення отримано на сервері!");
        stream.Write(response, 0, response.Length);
        Console.WriteLine("Відповідь надіслана до клієнта");

        while (true)
        {

            while(Console.KeyAvailable)
            {
                string massage = "";
                massage = Console.ReadLine();

                if(massage != "")
                {
                    byte[] _response = Encoding.UTF8.GetBytes(massage);
                    stream.Write(_response, 0, _response.Length);

                }
            }
            if(!reading)
                ServerRead();

        }
        */
    }
    public void StopServer()
    {
        client.Close();
        listener.Stop();
    }
    public void SendMessageToClient(string message)
    {
            byte[] _response = Encoding.UTF8.GetBytes(message);
            stream.Write(_response, 0, _response.Length);

    }

    public void StartClient()
    {
        IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
        int port = 12345;

        client = new TcpClient();

        client.Connect(ipAddress, port);
        Console.WriteLine("Підключення до сервера: {0}", client.Client.RemoteEndPoint);

        stream = client.GetStream();

        /*string message = "Привіт, сервер!";
        byte[] data = Encoding.UTF8.GetBytes(message);
        stream.Write(data, 0, data.Length);
        Console.WriteLine("Повідомлення надіслано до сервера: {0}", message);

        data = new byte[1024];
        int bytes = stream.Read(data, 0, data.Length);
        message = Encoding.UTF8.GetString(data, 0, bytes);
        Console.WriteLine("Відповідь сервера: {0}", message);

        while (true)
        {
            data = new byte[1024];
            int _bytes = stream.Read(data, 0, data.Length);
            message = Encoding.UTF8.GetString(data, 0, _bytes);
            Console.WriteLine("Відповідь сервера: {0}", message);
            while(true)
            {
                while (Console.KeyAvailable)
                {
                    string _massage = "";
                    _massage = Console.ReadLine();
                    if (_massage != "")
                    {
                        byte[] _data = Encoding.UTF8.GetBytes(_massage);
                        stream.Write(_data, 0, _data.Length);
                        Console.WriteLine("Повідомлення надіслано до сервера: {0}", _massage);
                    }
                }
                if(!reading)
                    ClientRead(stream);
            }
        }
        */
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
                //Console.WriteLine("Відповідь сервера: {0}", message);
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
            //Console.WriteLine("Клієнт надіслав повідомлення: {0}", _message);
            reading = false;
        });
    }

    public void SendMessageToServer(string message)
    {
        byte[] data = Encoding.UTF8.GetBytes(message);
        stream.Write(data, 0, data.Length);
    }
    public void SendMessage(string message)
    {
        if (isServer)
            SendMessageToClient(message);
        else
            SendMessageToServer(message);
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
}