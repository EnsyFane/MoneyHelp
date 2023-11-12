using MoneyHelp.Core;
using MoneyHelp.Core.Results;

namespace MoneyHelp.DataAccess.Abstractions.Errors;

public sealed record ChangesNotSavedError : Error
{
    public ChangesNotSavedError(IEnumerable<Error>? innerErrors = null) : base(ErrorKeys.ChangesNotSavedError, innerErrors: innerErrors)
    {
    }
}
