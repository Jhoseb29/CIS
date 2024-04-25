//-----------------------------------------------------------------------
// <copyright file="IAbstractApiException.cs" company="Jala University">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.Net;

namespace JalaU.CIS_API.System.Core.Domain;

/// <summary>
/// Represents an abstract base class for custom API exceptions.
/// </summary>
public interface IAbstractApiException
{
    /// <summary>
    /// Gets or sets the HTTP status code associated with the exception.
    /// </summary>
    /// <returns>HttpStatusCode.</returns>
    public HttpStatusCode StatusCode();
}
