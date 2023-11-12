namespace MoneyHelp.Core.Results;

public record Result
{
    public Error? Error { get; init; }

    public bool IsFailure => Error is not null;

    public static Result Success() => new() { };
    public static Result<T> Success<T>(T value) => new() { Value = value };

    public static Result Failure(Error error) => new() { Error = error };
    public static Result<T> Failure<T>(Error error) => new() { Error = error };
}

public sealed record Result<T> : Result
{
    public T? Value { get; init; }
}   