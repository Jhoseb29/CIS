using JalaU.CIS_API.System.Core.Domain;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace CIS_API;

/// <summary>
/// Provides functionality to handle exceptions that occur during HTTP request processing.
/// </summary>
public class ExceptionHandler(ILogger<ExceptionHandler> logger) : IExceptionHandler
{
    private readonly ILogger<ExceptionHandler> logger = logger;

    /// <inheritdoc/>
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken
    )
    {
        List<object> errorList = [];
        var errorMap = new Dictionary<string, object>();

        int statusCode = exception switch
        {
            IAbstractApiException abstractApiException => (int)abstractApiException.StatusCode(),
            _ => StatusCodes.Status500InternalServerError
        };

        if (exception is not WrongDataException)
        {
            var messageLogDTO = new MessageLogDTO(statusCode, exception.Message);
            errorList.Add(messageLogDTO);
        }
        else
        {
            var wrongDataException = (WrongDataException)exception;
            errorList.AddRange(wrongDataException.MessageLogs);
        }

        errorMap.Add("errors", errorList);
        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        await httpContext.Response.WriteAsJsonAsync(errorMap, cancellationToken: cancellationToken);
        return true;
    }
}
