//-----------------------------------------------------------------------
// <copyright file="WrongDataException.cs" company="Jala University">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.Net;

namespace JalaU.CIS_API.System.Core.Domain;

/// <summary>
/// Initializes a new instance of the <see cref="WrongDataException"/> class with a specified error message and a list of message log DTOs.
/// </summary>
/// <param name="message">The error message that explains the reason for the exception.</param>
/// <param name="messageLogDTOs">The list of message log DTOs associated with the exception.</param>
public class WrongDataException(string message, List<MessageLogDTO> messageLogDTOs)
    : ApplicationException(message),
        IAbstractApiException
{
    /// <inheritdoc/>
    public HttpStatusCode StatusCode() => HttpStatusCode.NotFound;

    /// <summary>
    /// Gets or sets the list of message log DTOs associated with the exception.
    /// </summary>
    public List<MessageLogDTO> MessageLogs { get; set; } = messageLogDTOs;
}
