namespace MoneyHelp.Core.Configuration;

public interface IConfig
{
    static string ConfigurationName { get; } = string.Empty;

    bool IsValid();
}
