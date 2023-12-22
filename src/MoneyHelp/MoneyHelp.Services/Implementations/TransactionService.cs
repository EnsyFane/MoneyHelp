using Microsoft.Extensions.Logging;

using MoneyHelp.Core.Models;
using MoneyHelp.Core.Results;
using MoneyHelp.DataAccess.Abstractions.Repositories;
using MoneyHelp.Services.Abstractions;
using MoneyHelp.Services.Models.Transactions;
using Db = MoneyHelp.DataAccess.Abstractions.Models;

namespace MoneyHelp.Services.Implementations;

internal sealed class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IWalletRepository _walletRepository;
    private readonly ITypeRepository _typeRepository;
    private readonly ILogger<TransactionService> _logger;
    
    public TransactionService(
        ITransactionRepository transactionRepository,
        IWalletRepository walletRepository,
        ITypeRepository typeRepository,
        ILogger<TransactionService> logger)
    {
        _transactionRepository = transactionRepository ?? throw new ArgumentNullException(nameof(transactionRepository));
        _walletRepository = walletRepository ?? throw new ArgumentNullException(nameof(walletRepository));
        _typeRepository = typeRepository ?? throw new ArgumentNullException(nameof(typeRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<Result<Transaction>> Add(TransactionCreate transactionCreate, CancellationToken ct)
    {
        var dbModel = transactionCreate.ToDbModel();
        var validationResult = await Validate(dbModel, ct);
        if (validationResult.IsFailure)
        {
            _logger.LogWarning("Transaction create validation failed: {Error}", validationResult.Error);
            return Result.Failure<Transaction>(validationResult.Error!);
        }

        var addResult = await _transactionRepository.Add(dbModel, ct);
        if (addResult.IsFailure)
        {
            return Result.Failure<Transaction>(addResult.Error!);
        }

        return Result.Success(addResult.Value!.ToDomainModel());
    }

    public Task<Result<IEnumerable<Transaction>>> GetAll(Guid userId, CancellationToken ct)
    {
        // TODO: Add filtering, sorting, pagination
        throw new NotImplementedException();
    }

    public async Task<Result<Transaction>> GetById(Guid userId, Guid id, CancellationToken ct)
    {
        var transaction = await _transactionRepository.GetById(userId, id, ct);
        if (transaction.IsFailure)
        {
            return Result.Failure<Transaction>(transaction.Error!);
        }

        // TODO?: Extend entity? (Add wallet instead of walletId)
        return Result.Success(transaction.Value!.ToDomainModel());
    }

    public async Task<Result> HardDelete(Guid userId, Guid id, CancellationToken ct)
    {
        var result = await _transactionRepository.HardDelete(userId, id, ct);
        if (result.IsFailure)
        {
            return Result.Failure(result.Error!);
        }

        return Result.Success();
    }

    public async Task<Result> SoftDelete(Guid userId, Guid id, CancellationToken ct)
    {
        var result = await _transactionRepository.SoftDelete(userId, id, ct);
        if (result.IsFailure)
        {
            return Result.Failure(result.Error!);
        }

        return Result.Success();
    }

    public async Task<Result> Update(TransactionUpdate transactionUpdate, CancellationToken ct)
    {
        var dbModel = transactionUpdate.ToDbModel();
        var validationResult = await Validate(dbModel, ct);
        if (validationResult.IsFailure)
        {
            _logger.LogWarning("Transaction update validation failed: {Error}", validationResult.Error);
            return Result.Failure<Transaction>(validationResult.Error!);
        }

        var updateResult = await _transactionRepository.Update(dbModel, ct);
        if (updateResult.IsFailure)
        {
            return Result.Failure<Transaction>(updateResult.Error!);
        }

        return Result.Success(dbModel.ToDomainModel());
    }

    private async Task<Result> Validate(Db.Transaction transaction, CancellationToken ct)
    {
        var wallet = await _walletRepository.GetById(transaction.UserId, transaction.WalletId, ct);
        if (wallet.IsFailure)
        {
            return Result.Failure(wallet.Error!);
        }

        var transactionType = await _typeRepository.GetById(transaction.UserId, transaction.TypeId, ct);
        if (transactionType.IsFailure)
        {
            return Result.Failure(transactionType.Error!);
        }

        return Result.Success();
    }
}
