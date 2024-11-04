namespace SyslogServer;

public class Server
{
    private Thread _receiverThread;

    public Server()
    {
        
        var receiver = new MessageReceiver(10514);
        receiver.OnCrash += s =>
        {
            if(_receiverThread is { IsAlive: true })
                _receiverThread.Interrupt();
            _receiverThread = new Thread(receiver.Run);
            _receiverThread.Start();
        };
        _receiverThread = new Thread(receiver.Run);

    }

    public void Run()
    {
        if (_receiverThread != null)
        {
            Console.WriteLine("Starting receiver...");
            _receiverThread.Start();
        }

        Console.WriteLine("Running...");
    }
}