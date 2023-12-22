using Microsoft.Extensions.DependencyInjection;

using MoneyHelp.Services.Abstractions;
using MoneyHelp.Services.Implementations;

namespace MoneyHelp.Services;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IWalletService, WalletService>()
            .AddScoped<ITypeService, TypeService>()
            .AddScoped<ITransactionService, TransactionService>();

        return services;
    }
}
