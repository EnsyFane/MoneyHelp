using MoneyHelp.Api.Services;
using MoneyHelp.Api.Services.Abstractions;

namespace MoneyHelp.Api;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IWalletService, WalletService>()
            .AddScoped<ITypeService, TypeService>()
            .AddScoped<ITransactionService, TransactionService>();

        return services;
    }
}
