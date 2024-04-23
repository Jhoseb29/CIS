//-----------------------------------------------------------------------
// <copyright file="VoteRequestDTO.cs" company="Jala University">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace JalaU.CIS_API.System.Core.Domain;

/// <summary>
/// Represents a DTO (Data Transfer Object) for a vote request.
/// </summary>
public class VoteRequestDTO : BaseRequestDTO
{
    /// <summary>
    /// Gets or sets a value indicating whether gets or sets the description of the idea.
    /// </summary>
    public bool Positive { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the topic associated with the idea.
    /// </summary>
    public required Guid TopicId { get; set; }
}
