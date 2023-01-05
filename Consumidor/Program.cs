using Confluent.Kafka;

var config = new ConsumerConfig
{
    GroupId = "Coleta",
    BootstrapServers = "localhost:9092"
};

using var consumer = new ConsumerBuilder<string, string>(config).Build();

consumer.Subscribe("coleta");

while(true)
{
    var result = consumer.Consume();
    Console.WriteLine($"Mensagem: { result.Message.Key } - { result.Message.Value }");
}