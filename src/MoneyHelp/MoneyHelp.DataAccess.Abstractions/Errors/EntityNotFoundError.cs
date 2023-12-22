using MoneyHelp.Core;
using MoneyHelp.Core.Results;

namespace MoneyHelp.DataAccess.Abstractions.Errors;

public sealed record EntityNotFoundError<T> : Error
{
    public EntityNotFoundError(IEnumerable<Error>? innerErrors = null) : base(ErrorKeys.EntityNotFoundError, innerErrors: innerErrors)
    {
    }
}
