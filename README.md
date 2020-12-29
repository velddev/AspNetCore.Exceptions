# AspNetCore.Exceptions
AspNetCore exception-based error handler

## Examples

### With attributes
For existing exceptions, a simple attribute to define the returning status code.

```cs
[StatusCode(400)]
class MyException : Exception 
{
  ...
}
```

### With parent types
To define a new exception which is going to be handled, use `HttpException` as your parent class.

```cs
class MyException : HttpException {
  public MyException()
    : base(400)
  {}
}
```

### With message
To create a user-friendly response message 

```cs
class MyException : HttpException, IExplainableException {
  public MyException()
    : base(400)
  {}

  object Explain() {
    return "Could not finish the example.";
  }
}
```
