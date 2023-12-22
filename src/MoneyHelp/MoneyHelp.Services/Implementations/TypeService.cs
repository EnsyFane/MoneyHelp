using Microsoft.Extensions.Logging;
using MoneyHelp.Core.Models;
using MoneyHelp.Core.Results;
using MoneyHelp.DataAccess.Abstractions.Errors;
using MoneyHelp.DataAccess.Abstractions.Repositories;
using MoneyHelp.Services.Abstractions;
using MoneyHelp.Services.Models.Types;
using Db = MoneyHelp.DataAccess.Abstractions.Models;

namespace MoneyHelp.Services.Implementations;

internal sealed class TypeService : ITypeService
{
    private readonly ITypeRepository _typeRepository;
    private readonly ILogger<TypeService> _logger;

    public TypeService(
        ITypeRepository typeRepository,
        ILogger<TypeService> logger)
    {
        _typeRepository = typeRepository ?? throw new ArgumentNullException(nameof(typeRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<Result<TransactionType>> Add(TypeCreate typeCreate, CancellationToken ct)
    {
        var dbModel = typeCreate.ToDbModel();
        var validationResult = await Validate(dbModel, ct);
        if (validationResult.IsFailure)
        {
            _logger.LogWarning("Type create validation failed: {Error}", validationResult.Error);
            return Result.Failure<TransactionType>(validationResult.Error!);
        }

        var addResult = await _typeRepository.Add(dbModel, ct);
        if (addResult.IsFailure)
        {
            return Result.Failure<TransactionType>(addResult.Error!);
        }

        return Result.Success(addResult.Value!.ToDomainModel());
    }

    public Task<Result<IEnumerable<TransactionType>>> GetAll(Guid userId, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<TransactionType>> GetById(Guid userId, Guid id, CancellationToken ct)
    {
        var transactionType = await _typeRepository.GetById(userId, id, ct);
        if (transactionType.IsFailure)
        {
            return Result.Failure<TransactionType>(transactionType.Error!);
        }

        return Result.Success(transactionType.Value!.ToDomainModel());
    }

    public async Task<Result> HardDelete(Guid userId, Guid id, CancellationToken ct)
    {
        var result = await _typeRepository.HardDelete(userId, id, ct);
        if (result.IsFailure)
        {
            return Result.Failure(result.Error!);
        }

        return Result.Success();
    }

    public async Task<Result> SoftDelete(Guid userId, Guid id, CancellationToken ct)
    {
        var result = await _typeRepository.SoftDelete(userId, id, ct);
        if (result.IsFailure)
        {
            return Result.Failure(result.Error!);
        }

        return Result.Success();
    }

    public async Task<Result> Update(TypeUpdate typeUpdate, CancellationToken ct)
    {
        var dbModel = typeUpdate.ToDbModel();
        var validationResult = await Validate(dbModel, ct);
        if (validationResult.IsFailure)
        {
            _logger.LogWarning("Type update validation failed: {Error}", validationResult.Error);
            return Result.Failure<TransactionType>(validationResult.Error!);
        }

        var updateResult = await _typeRepository.Update(dbModel, ct);
        if (updateResult.IsFailure)
        {
            return Result.Failure(updateResult.Error!);
        }

        return Result.Success();
    }

    private async Task<Result> Validate(Db.TransactionType type, CancellationToken ct)
    {
        var existingType = await _typeRepository.GetByNameExact(type.UserId, type.Name, ct);
        if (!existingType.IsFailure)
        {
            return Result.Failure<TransactionType>(new DuplicateEntityError<Db.TransactionType>());
        }

        if (existingType.Error is not EntityNotFoundError<Db.TransactionType>)
        {
            return Result.Failure<TransactionType>(existingType.Error!);
        }

        return Result.Success();
    }
}
