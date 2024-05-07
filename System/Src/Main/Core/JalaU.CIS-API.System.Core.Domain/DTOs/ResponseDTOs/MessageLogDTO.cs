//-----------------------------------------------------------------------
// <copyright file="MessageLogDTO.cs" company="Jala University">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace JalaU.CIS_API.System.Core.Domain;

/// <summary>
/// Represents a data transfer object (DTO) for message logging.
/// </summary>
public class MessageLogDTO(int statusCode, string message)
{
    /// <summary>
    /// Gets or sets the status code associated with the message log.
    /// </summary>
    public int StatusCode { get; set; } = statusCode;

    /// <summary>
    /// Gets or sets the message content of the log.
    /// </summary>
    public string Message { get; set; } = message;
}
