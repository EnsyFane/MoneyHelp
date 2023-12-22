using MoneyHelp.Core;
using MoneyHelp.Core.Results;

namespace MoneyHelp.DataAccess.Abstractions.Errors;

public sealed record DuplicateEntityError<T> : Error
{
    public DuplicateEntityError(IEnumerable<Error>? innerErrors = null) : base(ErrorKeys.DuplicateEntityError, innerErrors: innerErrors)
    {
    }
}
