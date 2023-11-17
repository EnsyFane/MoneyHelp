using MoneyHelp.Core.Models;
using MoneyHelp.Core.Results;

namespace MoneyHelp.Api.Services.Abstractions;

internal interface IWalletService
{
    Task<Result<Wallet>> GetById(Guid userId, Guid id, CancellationToken ct);
    Task<Result<IEnumerable<Wallet>>> GetAll(Guid userId, CancellationToken ct);
    Task<Result<Wallet>> Add(Wallet entity, CancellationToken ct);
    Task<Result> Update(Wallet entity, CancellationToken ct);
    Task<Result> SoftDelete(Guid userId, Guid id, CancellationToken ct);
    Task<Result> HardDelete(Guid userId, Guid id, CancellationToken ct);
}
