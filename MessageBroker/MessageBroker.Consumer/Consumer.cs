using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace MessageBroker.Consumer
{
    public class Consumer
    {
        private static ConnectionFactory _factory;
        private static IConnection _connection;

        private const string HUAWEI_EXCHANGE = "HuaweiExchange";
        private const string CardPaymentQueueName = "CardPaymentTopic_Queue";

        public void CreateConnection()
        {
            _factory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };
        }

        public void Close()
        {
            _connection.Close();
        }

        public void ProcessMessages()
        {
            do
            {
                using (_connection = _factory.CreateConnection())
                {
                    using (var channel = _connection.CreateModel())
                    {
                        channel.QueueDeclare("huawei-relation",
                            false, false, false, null);

                        var consumer = new EventingBasicConsumer(channel);
                        consumer.Received += (model, ea) =>
                        {
                            var body = ea.Body.ToArray();
                            var message = Encoding.UTF8.GetString(body);
                        };

                        channel.BasicConsume(queue: "task_queue",
                         autoAck: true,
                         consumer: consumer);
                    }
                }
            } while (true);
        }
    }
}
