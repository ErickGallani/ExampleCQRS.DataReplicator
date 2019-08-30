namespace ExampleCQRS.DataReplicator.Consumer
{
    using Microsoft.Extensions.Configuration;
    using RabbitMQ.Client;

    public class Bootstrap
    {
        private IConnection connection;
        private IModel channel;

        public void Setup(IConfigurationRoot configuration)
        {
            var messageBroke = configuration.GetSection("MessageBroke");

            var host = messageBroke.GetSection("Host").Value;
            var port = int.Parse(messageBroke.GetSection("Port").Value);
            var queueName = messageBroke.GetSection("Queue").Value;
            var user = messageBroke.GetSection("User").Value;
            var password = messageBroke.GetSection("Password").Value;

            var factory = new ConnectionFactory
            {
                HostName = host,
                Port = port,
                UserName = user,
                Password = password
            };

            connection = factory.CreateConnection();
            channel = connection.CreateModel();
            
            channel.QueueDeclare(queue: queueName,
                                    durable: false,
                                    exclusive: false,
                                    autoDelete: false,
                                    arguments: null);

            var messageReceiver = new MessageReceiver(channel);

            channel.BasicConsume(messageReceiver, queueName);
        }

        public void Stop()
        {
            channel?.Close();
            channel = null;

            connection?.Close();
            connection = null;
        }
    }
}
