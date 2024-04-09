//-----------------------------------------------------------------------
// <copyright file="Vote.cs" company="Jala University">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JalaU.CIS_API.System.Core.Domain;

/// <summary>
/// Represents a vote entity.
/// </summary>
public class Vote
{
    /// <summary>
    /// Gets or sets the unique identifier of the vote.
    /// </summary>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public required Guid Id { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the vote is positive or negative.
    /// </summary>
    [Column("positive")]
    public required bool Positive { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the user who cast the vote.
    /// </summary>
    [Column("userId")]
    public required Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the idea that the vote is associated with.
    /// </summary>
    [Column("ideaId")]
    public required Guid IdeaId { get; set; }
}
