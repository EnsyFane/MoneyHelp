namespace MoneyHelp.Core.Results;

public abstract record Error
{
    public string ErrorKey { get; init; }
    public string? Message { get; init; }
    public IEnumerable<Error>? InnerErrors { get; init; }

    protected Error(string errorKey, string? message = null, IEnumerable<Error>? innerErrors = null)
    {
        Message = message;
        ErrorKey = errorKey;
        InnerErrors = innerErrors;
    }
}
