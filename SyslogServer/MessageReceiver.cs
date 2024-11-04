using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SyslogServer;
public class MessageReceiver
{
    private readonly int _port;

    public delegate void CrashDelegate(string m);
    public event CrashDelegate? OnCrash;
    
    public MessageReceiver(int port)
    {
        _port = port;

    }

    public void Run()
    {
        Console.WriteLine("Running receiver...");
        var udpListener = new UdpClient(_port);

        try
        {
            IPEndPoint endpoint = new IPEndPoint(IPAddress.Any, _port);
            string receivedData;
            byte[] receivedBytes;

            while (true)
            {
                receivedBytes = udpListener.Receive(ref endpoint);
                receivedData = Encoding.ASCII.GetString(receivedBytes, 0, receivedBytes.Length);
                Console.WriteLine($"From ip: {endpoint.Address.ToString()} in {DateTime.Now} received: {receivedData}");
            }
        }
        catch (Exception e)
        {
            udpListener.Close();
            Console.WriteLine(e.Message);
            Thread.Sleep(3000);
            Console.WriteLine("New atempt to run receiver...");
            OnCrash?.Invoke(e.Message);
        }
    }
}