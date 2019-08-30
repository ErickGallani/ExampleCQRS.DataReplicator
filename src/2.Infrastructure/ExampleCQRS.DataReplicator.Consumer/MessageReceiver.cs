namespace ExampleCQRS.DataReplicator.Consumer
{
    using RabbitMQ.Client;
    using System;
    using System.Text;

    public class MessageReceiver : DefaultBasicConsumer
    {
        private readonly IModel channel;

        public MessageReceiver(IModel channel) => this.channel = channel;

        public override void HandleBasicDeliver(
            string consumerTag, 
            ulong deliveryTag, 
            bool redelivered, 
            string exchange, 
            string routingKey, 
            IBasicProperties properties, 
            byte[] body)
        {

            Console.WriteLine("Consuming Message");
            Console.WriteLine($"Message received from the exchange {exchange}");
            Console.WriteLine($"Consumer tag: {consumerTag}");
            Console.WriteLine($"Delivery tag: {deliveryTag}");
            Console.WriteLine($"Routing tag: {routingKey}");
            Console.WriteLine($"Message: {Encoding.UTF8.GetString(body)}");

            channel.BasicAck(deliveryTag, false);
        }
    }
}
