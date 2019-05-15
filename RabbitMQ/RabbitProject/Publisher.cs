using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitProject
{
    public class Publisher
    {
        private readonly RabbitMQService _rabbitMQService;

        public Publisher(string queueName, string message)
        {
            _rabbitMQService = new RabbitMQService();

            using (var connection = _rabbitMQService.GetRabbitMQConnection())
            {
                // CreateModel methodunu çağırarak, RabbitMQ üzerinde yeni bir channel/session yaratmaktayız.
                // Bu Channel sayesinde bir Queue oluşturabilirken, mesaj gönderme işlemlerini de gerçekleştirebilmekteyiz.
                using (var channel = connection.CreateModel())
                {
                    // yeni bir queue tanımlıyoruz.
                    // durable: Bu parametre ile in-memory olarak çalışan Queue disk üzerinden çalışmaya başlayacaktır. 
                    // Bu sayede RabbitMQ servisi dursa bile Queue kaybolmayacaktır. 
                    // Her güzelliğin getirdiği bir kötü tarafın olduğu gibi bununda beraberinde getireceği latency problemi bulunmaktadır haliyle.
                    channel.QueueDeclare(queueName, false, false, false, null);

                    // BasicPublish methodu ile kolay bir şekilde oluşturmuş olduğumuz ilgili Queue’ya mesaj gönderiyoruz
                    // exchange: Bu parametreyi es geçiyoruz. Exchange genel olarak mesajı ilgili Routing Key’e göre ilgili Queue’ya yönlendiren bölümdür. 
                    // Direct Exchange, Fanout Exchange ve Topic Exchange gibi tipleri bulunmaktadır. Bunları bir sonraki makalemde detaylı olarak ele alacağım.
                    // routingKey: Burada girmiş olduğumuz key’e göre ilgili Queue’ya yönlendirilecektir mesaj.
                    // Queue’ya göndermek istediğimiz mesajı byte[] tipinde gönderiyoruz.
                    channel.BasicPublish("", queueName, null, Encoding.UTF8.GetBytes(message));

                    Console.WriteLine("{0} queue'su üzerine, \"{1}\" mesajı yazıldı.", queueName, message);
                }
            }
        }
    }
}
