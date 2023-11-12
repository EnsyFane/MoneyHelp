using MoneyHelp.Core;
using MoneyHelp.Core.Results;

namespace MoneyHelp.DataAccess.Abstractions.Errors;

public sealed record TooManyRowsAffectedError : Error
{
    public TooManyRowsAffectedError(IEnumerable<Error>? innerErrors = null) : base(ErrorKeys.TooManyRowsAffectedError, innerErrors: innerErrors)
    {
    }
}
