using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.IO;

public class Program
{
    public static void Main(string[] args)
    {
        int sd = int.Parse(Console.ReadLine());
        switch (sd)
        {
            case 0:
                Server();
                break;

            case 1:
                Client();
                break;
        }
    }

    public static void Server()
    {
        IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
        int port = 12345;

        TcpListener listener = new TcpListener(ipAddress, port);

        listener.Start();
        Console.WriteLine("Сервер запущено на адресі {0}:{1}", ipAddress, port);

        TcpClient client = listener.AcceptTcpClient();
        Console.WriteLine("Клієнт підключився: {0}", client.Client.RemoteEndPoint);

        NetworkStream stream = client.GetStream();

        byte[] data = new byte[1024];
        int bytesRead = stream.Read(data, 0, data.Length);
        string message = Encoding.UTF8.GetString(data, 0, bytesRead);
        Console.WriteLine("Клієнт надіслав повідомлення: {0}", message);

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
            ServerRead(stream);

        }

        client.Close();
        listener.Stop();
    }
    static public async void ServerRead(NetworkStream stream) => await Task.Run(() =>
        {
            byte[] _data = new byte[1024];
            int _bytesRead = stream.Read(_data, 0, _data.Length);
            string _message = Encoding.UTF8.GetString(_data, 0, _bytesRead);
            Console.WriteLine("Клієнт надіслав повідомлення: {0}", _message);
        });


    public static void Client()
    {
        IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
        int port = 12345;

        TcpClient client = new TcpClient();

        client.Connect(ipAddress, port);
        Console.WriteLine("Підключення до сервера: {0}", client.Client.RemoteEndPoint);

        NetworkStream stream = client.GetStream();

        string message = "Привіт, сервер!";
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
                ClientRead(stream);
            }
        }



        client.Close();
    }
    static public async void ClientRead(NetworkStream stream) => await Task.Run(() =>
    {
        byte[] data = new byte[1024];
        int bytes = stream.Read(data, 0, data.Length);
        string message = Encoding.UTF8.GetString(data, 0, bytes);
        Console.WriteLine("Відповідь сервера: {0}", message);
    });


}