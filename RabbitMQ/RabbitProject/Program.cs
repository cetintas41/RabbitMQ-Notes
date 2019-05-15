using System;

namespace RabbitProject
{
    class Program
    {
        private static readonly string _queueName = "0f8fad5b-d9cb-469f-a165-70867728950e";
        private static Publisher _publisher;
        private static Consumer _consumer;

        static void Main(string[] args)
        {
            //_publisher = new Publisher(_queueName, "Hello RabbitMQ World!");
            _consumer = new Consumer(_queueName);
            Console.ReadKey();
        }
    }
}

/* *************************** DB SCHEMA ******************************
 
  -------------------------------Queues--------------------------------  
     Id     Name (Guid)                                  CreatedOn
     1      0f8fad5b-d9cb-469f-a165-70867728950e         2019-05-15 10:10:15
     2      7c9e6679-7425-40de-944b-e07fc1f90ae7         2019-05-16 16:19:55

  -------------------------------Users---------------------------------
    Id      Name                RegisteredOn
    1       Lionel Messi        2018-05-15 10:10:15
    2       Cristiano Ronaldo   2019-01-15 13:10:15
    3       Donald Duck         2019-11-15 13:10:15
    
  -------------------------------UsersQueues------------------------------
    UserId    QueueId     IsDeleted
    1         1           false
    2         1           false
    2         2           false 
    3         2           false
     
   -------------------------------Messages---------------------------------
   Id       QueueId     Content             SenderId        ReceiverId      SentOn
   1        1           Hello!              1               2               2019-05-17 10:10:15
   2        1           Hello too.          2               1               2019-05-18 10:10:15
   3        2           Are u ready?        3               2               2019-05-18 11:10:15
   4        2           Yes bro!            2               3               2019-05-18 13:10:15


*/
