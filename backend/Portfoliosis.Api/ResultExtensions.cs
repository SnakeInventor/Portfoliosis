using Portfoliosis.Core.Errors;

namespace Portfoliosis.Api;

public static class ResultExtensions
{
    public static IResult ToProblemDetails(this Result result)
    {
        if (result.IsSuccess) throw new InvalidOperationException("No problem details in successful result.");

        return Results.Problem(
            statusCode: result.Error.Type.ToStatusCode(),
            title: "Bad Request",
            type: "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
            extensions: new Dictionary<string, object?>()
            {
                { "errors", new [] { result.Error } }
            }
        );
    }

    private static int ToStatusCode(this ErrorType type) => type switch
    {
        ErrorType.Failure => StatusCodes.Status500InternalServerError,
        ErrorType.Validation => StatusCodes.Status400BadRequest,
        ErrorType.NotFound => StatusCodes.Status404NotFound,
        ErrorType.Conflict => StatusCodes.Status409Conflict
    };

}
