﻿using MoneyHelp.Api.Services.Abstractions;
using MoneyHelp.Core.Models;
using MoneyHelp.Core.Results;

namespace MoneyHelp.Api.Services;

internal sealed class WalletService : IWalletService
{
    public Task<Result<Wallet>> Add(Wallet entity, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<Result<IEnumerable<Wallet>>> GetAll(Guid userId, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<Result<Wallet>> GetById(Guid userId, Guid id, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<Result> HardDelete(Guid userId, Guid id, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<Result> SoftDelete(Guid userId, Guid id, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<Result> Update(Wallet entity, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
