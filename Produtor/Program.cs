var schemaConfig = new SchemaRegistryConfig
{
    Url = "http://localhost:8081"
};

var schemaRegistry = new CachedSchemaRegistryClient(schemaConfig);

var config = new ProducerConfig { BootstrapServers = "localhost:9092" };

var messageJson = new Models.ExibicaoPDP
{
        DeviceId = Guid.NewGuid().ToString(),
        Bandeira = "Casas Bahia",
        ProdutoId = "12345",
        PrecoSite = 199.99,
        PrecoDesconto = 0.99,
        Sku = "54321",
        Origem = "app-mobile-teste",
        DataEvento = $"{DateTime.Now.ToString()}",
        Usuario = new Models.Usuario
        {
            Id = Guid.NewGuid().ToString()
        }
};

var payload = System.Text.Json.JsonSerializer.Serialize(messageJson);

using var producer = new ProducerBuilder<string, string>(config)
                         .Build();

var result = await producer.ProduceAsync("coleta", new Message<string, string> 
{
    Key = Guid.NewGuid().ToString(),
    Value = payload
});

Console.WriteLine($"{result.Offset}");