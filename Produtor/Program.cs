var schemaConfig = new SchemaRegistryConfig
{
    Url = "http://localhost:8081"
};

var schemaRegistry = new CachedSchemaRegistryClient(schemaConfig);

var config = new ProducerConfig { BootstrapServers = "localhost:9092" };

using var producer = new ProducerBuilder<string, Models.Coleta>(config)
                        .SetValueSerializer(new AvroSerializer<Models.Coleta>(schemaRegistry))
                        .Build();

var message = new Message<string, Models.Coleta>
{
    Key = Guid.NewGuid().ToString(),
    Value = new Models.Coleta
    {
        DeviceId = Guid.NewGuid().ToString(),
        Bandeira = "Casas Bahia",
        ProdutoId = "12345",
        PrecoSite = 199.99,
        PrecoDesconto = 0.99,
        Sku = "54321",
        Origem = "origem",
        DataEvento = $"{DateTime.Now.ToString()}"
    }
};

var result = await producer.ProduceAsync("coleta", message);

Console.WriteLine($"{ result.Offset }");