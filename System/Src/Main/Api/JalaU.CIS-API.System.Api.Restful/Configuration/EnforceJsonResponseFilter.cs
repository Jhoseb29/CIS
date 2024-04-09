//-----------------------------------------------------------------------
// <copyright file="EnforceJsonResponseFilter.cs" company="Jala University">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace JalaU.CIS_API.System.Api.Restful;

using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Net.Http.Headers;

/// <summary>
/// An action filter attribute to enforce JSON response content type.
/// </summary>
[AttributeUsage(AttributeTargets.All)]
public class EnforceJsonResponseFilter : Attribute, IActionFilter
{
    /// <summary>
    /// This method is called after the action method is executed.
    /// It sets the response content type to "application/json" if the result is an ObjectResult.
    /// </summary>
    /// <param name="context">The context of the action being executed.</param>
    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Result is ObjectResult objectResult)
        {
            context.HttpContext.Response.ContentType = "application/json";
        }
    }

    /// <summary>
    /// This method is called before the action method is executed.
    /// It does nothing in this implementation.
    /// </summary>
    /// <param name="context">The context of the action being executed.</param>
    public void OnActionExecuting(ActionExecutingContext context)
    {
        // This is not necessary.
    }
}
