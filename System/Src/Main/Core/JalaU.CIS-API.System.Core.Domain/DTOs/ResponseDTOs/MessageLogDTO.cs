//-----------------------------------------------------------------------
// <copyright file="MessageLogDTO.cs" company="Jala University">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------S
namespace JalaU.CIS_API.System.Core.Domain;

/// <summary>
/// Represents a data transfer object (DTO) for message logging.
/// </summary>
public class MessageLogDTO
{
    /// <summary>
    /// Gets or sets the status code associated with the message log.
    /// </summary>
    public required int StatusCode { get; set; }

    /// <summary>
    /// Gets or sets the message content of the log.
    /// </summary>
    public required string Message { get; set; }
}
