namespace MoneyHelp.Core.Configuration;

public sealed record DbConfig : IConfig
{
    public static string ConfigurationName => "Db";

    public string ConnectionString { get; init; } = string.Empty;
    public string Schema { get; init; } = string.Empty;

    public bool IsValid()
        => !string.IsNullOrWhiteSpace(ConnectionString)
            && !string.IsNullOrWhiteSpace(Schema);
}
