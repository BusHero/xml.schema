namespace xml.schema;

public abstract record Result<T>
{
    public record Success(T Value) : Result<T>;
    public record Failure(string Message) : Result<T>;
    
    public static Result<T> CreateSuccess(T value) => new Success(value);
    public static Result<T> CreateFailure(string message) => new Failure(message);
}
