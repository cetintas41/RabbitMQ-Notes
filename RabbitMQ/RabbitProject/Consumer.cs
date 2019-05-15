using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitProject
{
    public class Consumer
    {
        private readonly RabbitMQService _rabbitMQService;

        public Consumer(string queueName)
        {
            _rabbitMQService = new RabbitMQService();

            using (var connection = _rabbitMQService.GetRabbitMQConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    // Asıl işlemlerimizi gerçekleştirecek oldıuğumuz “consumer” nesnemizdir. 
                    var consumer = new EventingBasicConsumer(channel);

                    // Received event'i sürekli listen modunda olacaktır.
                    // Queue’daki ilgili mesajları sırasıyla almaktadır ve “Body” property’sinde barındırmaktadır. 
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);

                        Console.WriteLine("{0} isimli queue üzerinden gelen mesaj: \"{1}\"", queueName, message);
                    };


                    // basic bir şekilde verilmiş olan Queue ismine göre mesajları alma işlemini başlatıyoruz. 
                    // queue: Hangi Queue’nun mesajları alınacak ise.
                    // noAck: True olarak set edildiği taktirde, consumer mesajı aldığı zaman otomatik olarak mesaj Queue’dan silinecektir. 
                    channel.BasicConsume(queueName, false, consumer);
                    Console.ReadLine();
                }
            }
        }
    }
}
