using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Net.Http.Headers;

namespace JalaU.CIS_API.System.Api.Restful;

[AttributeUsage(AttributeTargets.All)]
public class EnforceJsonResponseFilter : Attribute, IActionFilter
{
    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Result is ObjectResult objectResult)
        {
            context.HttpContext.Response.ContentType = "application/json";
        }
    }

    public void OnActionExecuting(ActionExecutingContext context) { }
}
