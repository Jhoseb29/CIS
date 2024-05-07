//-----------------------------------------------------------------------
// <copyright file="TopicRequestDTO.cs" company="Jala University">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace JalaU.CIS_API.System.Core.Domain;

/// <summary>
/// Represents a DTO (Data Transfer Object) for a topic request.
/// </summary>
public class TopicRequestDTO : BaseRequestDTO
{
    /// <summary>
    /// Gets or sets the title of the topic.
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// Gets or sets the description of the topic.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the labels associated with the topic.
    /// </summary>
    public List<string>? Labels { get; set; }
}
