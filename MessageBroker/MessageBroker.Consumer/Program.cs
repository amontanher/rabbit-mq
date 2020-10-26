using System;

namespace MessageBroker.Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            Consumer consumer = new Consumer();
            consumer.CreateConnection();
            consumer.ProcessMessages();
        }
    }
}
