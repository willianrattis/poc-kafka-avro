var config = new ConsumerConfig
{
    GroupId = "Coleta",
    BootstrapServers = "localhost:9092",
    EnableAutoCommit = false,
    EnablePartitionEof = true
};

using var consumer = new ConsumerBuilder<string, string>(config)
                         .Build();

consumer.Subscribe("coleta");

while (true)
{
    var result = consumer.Consume();

    if (result.IsPartitionEOF)
    {
        continue;
    }

    var payload = System.Text.Json.JsonSerializer.Deserialize<Models.ExibicaoPDP>(result.Message.Value, new JsonSerializerOptions
    {
        IgnoreReadOnlyProperties = true,
        IgnoreReadOnlyFields = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        PropertyNameCaseInsensitive = true,
        DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
    });

    var json = System.Text.Json.JsonSerializer.Serialize<Models.ExibicaoPDP>(payload, new JsonSerializerOptions{
        WriteIndented = true
    });

    Console.WriteLine(json);

    consumer.Commit();
}