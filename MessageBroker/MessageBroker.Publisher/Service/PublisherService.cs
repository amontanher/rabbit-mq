using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Text;

namespace MessageBroker.Publisher.Service
{
    public class PublisherService : IDisposable
    {
        private static ConnectionFactory _factory;
        private static IConnection _connection;
        private static IModel _channel;
        
        public PublisherService()
        {
            CreateConnection();
        }

        private static void CreateConnection()
        {
            _factory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };

            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();
            
            _channel.QueueDeclare("huawei-relation", false, false, false, null);
        }
        
        public void SendMessage<T>(T model)
        {
            var messageBody = SerializeModel(model);
            
            _channel.BasicPublish(exchange: "",
                routingKey: "task_queue",
                basicProperties: null,
                body: messageBody);
        }

        private byte[] SerializeModel<T>(T model)
        {
            var message = JsonConvert.SerializeObject(model);
            return Encoding.UTF8.GetBytes(message);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
