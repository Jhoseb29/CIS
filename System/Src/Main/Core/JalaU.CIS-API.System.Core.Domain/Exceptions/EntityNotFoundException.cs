//-----------------------------------------------------------------------
// <copyright file="EntityNotFoundException.cs" company="Jala University">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace JalaU.CIS_API.System.Core.Domain;

/// <summary>
/// Represents an exception that is thrown when an entity is not found.
/// </summary>
public class EntityNotFoundException(string message)
    : ApplicationException(message),
        IAbstractApiException
{
    /// <inheritdoc/>
    public HttpStatusCode StatusCode() => HttpStatusCode.NotFound;
}
