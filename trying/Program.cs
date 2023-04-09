using System;
using System.Net.Sockets;
using System.Net;
using System.Text;

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
        // Встановлення IP-адреси та порту сервера
        IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
        int port = 12345;

        // Створення TCP-сокету
        TcpListener listener = new TcpListener(ipAddress, port);

        // Початок прослуховування вхідних підключень
        listener.Start();
        Console.WriteLine("Сервер запущено на адресі {0}:{1}", ipAddress, port);

        // Прийняття підключення
        TcpClient client = listener.AcceptTcpClient();
        Console.WriteLine("Клієнт підключився: {0}", client.Client.RemoteEndPoint);

        // Отримання потоку для читання від клієнта
        NetworkStream stream = client.GetStream();

        // Читання даних від клієнта
        byte[] data = new byte[1024];
        int bytesRead = stream.Read(data, 0, data.Length);
        string message = Encoding.UTF8.GetString(data, 0, bytesRead);
        Console.WriteLine("Клієнт надіслав повідомлення: {0}", message);

        // Відправка даних клієнту
        byte[] response = Encoding.UTF8.GetBytes("Повідомлення отримано на сервері!");
        stream.Write(response, 0, response.Length);
        Console.WriteLine("Відповідь надіслана до клієнта");

        while (true)
        {
            string massage = "";
            massage = Console.ReadLine();

            if(massage != "")
            {
                byte[] _response = Encoding.UTF8.GetBytes(massage);
                stream.Write(_response, 0, _response.Length);

            }
        }

        // Закриття з'єднання
        client.Close();
        listener.Stop();
    }
    public static void Client()
    {
        // Встановлення IP-адреси та порту сервера
        Console.Write("IP:");
        string ip = Console.ReadLine();
        IPAddress ipAddress = IPAddress.Parse(ip);
        //IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
        int port = 12345;

        // Створення TCP-клієнта
        TcpClient client = new TcpClient();

        // Підключення до сервера
        client.Connect(ipAddress, port);
        Console.WriteLine("Підключення до сервера: {0}", client.Client.RemoteEndPoint);

        // Отримання потоку для читання та запису даних
        NetworkStream stream = client.GetStream();

        // Надсилання даних на сервер
        string message = "Привіт, сервер!";
        byte[] data = Encoding.UTF8.GetBytes(message);
        stream.Write(data, 0, data.Length);
        Console.WriteLine("Повідомлення надіслано до сервера: {0}", message);

        // Отримання даних від сервера
        while (true)
        {

            data = new byte[1024];
            int bytes = stream.Read(data, 0, data.Length);
            message = Encoding.UTF8.GetString(data, 0, bytes);
            Console.WriteLine("Відповідь сервера: {0}", message);
        }



        // Закриття з'єднання
        client.Close();
    }
}