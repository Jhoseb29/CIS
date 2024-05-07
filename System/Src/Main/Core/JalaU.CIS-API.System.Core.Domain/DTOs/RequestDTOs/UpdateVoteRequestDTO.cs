//-----------------------------------------------------------------------
// <copyright file="UpdateVoteRequestDTO.cs" company="Jala University">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace JalaU.CIS_API.System.Core.Domain;

/// <summary>
/// Represents a DTO (Data Transfer Object) for a vote request.
/// </summary>
public class UpdateVoteRequestDTO : BaseRequestDTO
{
    /// <summary>
    /// Gets or sets a value indicating whether it gets or sets the description of the idea.
    /// </summary>
    public bool Positive { get; set; }
}