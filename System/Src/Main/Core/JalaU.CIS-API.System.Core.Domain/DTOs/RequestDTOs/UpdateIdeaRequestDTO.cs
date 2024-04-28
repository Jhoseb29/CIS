//-----------------------------------------------------------------------
// <copyright file="UpdateIdeaRequestDTO.cs" company="Jala University">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace JalaU.CIS_API.System.Core.Domain;

/// <summary>
/// Represents a DTO (Data Transfer Object) for an idea request.
/// </summary>
public class UpdateIdeaRequestDTO : BaseRequestDTO
{
    /// <summary>
    /// Gets or sets the title of the idea.
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// Gets or sets the description of the idea.
    /// </summary>
    public string? Description { get; set; }
}