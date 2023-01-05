using Confluent.Kafka.SyncOverAsync;

var schemaConfig = new SchemaRegistryConfig
{
    Url = "http://localhost:8081"
};

var schemaRegistry = new CachedSchemaRegistryClient(schemaConfig);

var config = new ConsumerConfig
{
    GroupId = "Coleta",
    BootstrapServers = "localhost:9092"
};

using var consumer = new ConsumerBuilder<string, Models.Coleta>(config)
                         .SetValueDeserializer(new AvroDeserializer<Models.Coleta>(schemaRegistry)
                         .AsSyncOverAsync())
                         .Build();

consumer.Subscribe("coleta");

while (true)
{
    var result = consumer.Consume();
    WriteObject(result);
}

static void WriteObject(ConsumeResult<string, Models.Coleta>? result)
{
    Console.WriteLine();
    Console.WriteLine($"Key: {result?.Message?.Key}");
    Console.WriteLine("----------------------------------------------");
    Console.WriteLine($"DeviceId: {result?.Message.Value?.DeviceId}");
    Console.WriteLine($"Bandeira: {result?.Message.Value?.Bandeira}");
    Console.WriteLine($"ProdutoId: {result?.Message.Value?.ProdutoId}");
    Console.WriteLine($"PrecoSite: {result?.Message.Value?.PrecoSite}");
    Console.WriteLine($"PrecoDesconto: {result?.Message.Value?.PrecoDesconto}");
    Console.WriteLine($"Sku: {result?.Message.Value?.Sku}");
    Console.WriteLine($"Origem: {result?.Message.Value?.Origem}");
    Console.WriteLine($"DataEvento: {result?.Message.Value?.DataEvento}");
}