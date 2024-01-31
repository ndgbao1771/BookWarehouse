/*using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
//Here we specify the Rabbit MQ Server. we use rabbitmq docker image and use it
var factory = new ConnectionFactory
{
    HostName = "localhost"
};
//Create the RabbitMQ connection using connection factory details as i mentioned above
var connection = factory.CreateConnection();
//Here we create channel with session and model
using
var channel = connection.CreateModel();
//declare the queue after mentioning name and a few property related to that
channel.QueueDeclare("book", exclusive: false);
//Set Event object which listen message from chanel which is sent by producer
var consumer = new EventingBasicConsumer(channel);
consumer.Received += (model, eventArgs) => {
    var body = eventArgs.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine($"Book message received: {message}");
};
//read the message
channel.BasicConsume(queue: "book", autoAck: true, consumer: consumer);
Console.ReadKey();*/

using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using Newtonsoft.Json;


internal class Program
{
    private static void Main()
    {
        var factory = new ConnectionFactory
        {
            HostName = "localhost"
        };
        var connection = factory.CreateConnection();

        using (var channel = connection.CreateModel())
        {
            channel.QueueDeclare("book", exclusive: false);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, eventArgs) =>
            {
                var body = eventArgs.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"Book message received: ");
                // Deserializing JSON to a list of objects
                var books = JsonConvert.DeserializeObject<List<Book>>(message);

                // Displaying formatted information
                foreach (var book in books)
                {
                    Console.WriteLine($"Book ID: {book.Id}" + " - " 
                                    + $"Name: {book.Name}" + " - " 
                                    + $"Seri: {book.Seri}" + " - " 
                                    + $"Author: {book.Author}" + " - " 
                                    + $"Category: {book.BookCategory}");
                }
            };

            // Read the message
            channel.BasicConsume(queue: "book", autoAck: true, consumer: consumer);

            Console.ReadKey();
        }
    }
}

public class Book
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Seri { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string UpdatedBy { get; set; }
    public string Author { get; set; }
    public string BookCategory { get; set; }
}