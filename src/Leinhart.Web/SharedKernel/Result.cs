namespace Leinhart.Web.SharedKernel;

public class Result
{
    public bool HasError { get; }
    public string? Message { get; }
    public ResultType Type { get; }

    protected Result(bool hasError, string? message, ResultType type)
    {
        HasError = hasError;
        Message = message;
        Type = type;
    }

    public static Result Success(ResultType type = ResultType.Ok)
    {
        return new Result(false, null, type);
    }

    public static Result Failure(string message, ResultType type = ResultType.UnexpectedError)
    {
        return new Result(true, message, type);
    }
}

public class Result<T> : Result
{
    public T? Value { get; }

    private Result(bool hasError, T? value, string? message, ResultType type)
        : base(hasError, message, type)
    {
        Value = value;
    }

    public static Result<T> Success(T value, ResultType type = ResultType.Ok)
    {
        return new Result<T>(false, value, null, type);
    }

    public new static Result<T> Failure(string message, ResultType type = ResultType.UnexpectedError)
    {
        return new Result<T>(true, default, message, type);
    }
}