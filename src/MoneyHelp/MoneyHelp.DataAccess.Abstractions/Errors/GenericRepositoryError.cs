using MoneyHelp.Core;
using MoneyHelp.Core.Results;

namespace MoneyHelp.DataAccess.Abstractions.Errors;

public sealed record GenericRepositoryError : Error
{
    private const string MessageTemplate = "Exception: {0}. Message: {1}";

    public GenericRepositoryError(Exception ex) : base(ErrorKeys.GenericRepositoryError, string.Format(MessageTemplate, ex.GetType().Name, ex.Message))
    {
    }
}
