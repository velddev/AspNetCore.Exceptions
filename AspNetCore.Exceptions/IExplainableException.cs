namespace AspNetCore.ExceptionHandler;

public interface IExplainableException
{
    public object Explain();
}