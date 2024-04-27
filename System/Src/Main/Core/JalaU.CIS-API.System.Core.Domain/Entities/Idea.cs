//-----------------------------------------------------------------------
// <copyright file="Idea.cs" company="Jala University">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JalaU.CIS_API.System.Core.Domain;

/// <summary>
/// Represents an idea entity.
/// </summary>
public class Idea
{
    /// <summary>
    /// Gets or sets the unique identifier of the idea.
    /// </summary>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public required Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the title of the idea.
    /// </summary>
    [Column("title")]
    [StringLength(200)]
    public required string? Title { get; set; }

    /// <summary>
    /// Gets or sets the description of the idea.
    /// </summary>
    [Column("description")]
    [StringLength(500)]
    public required string? Description { get; set; }

    /// <summary>
    /// Gets or sets the date when the idea was created.
    /// </summary>
    [Column("date")]
    public required DateTime Date { get; set; } = DateTime.Now;

    /// <summary>
    /// Gets or sets the unique identifier of the user who created the idea.
    /// </summary>
    [Column("userId")]
    public required Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the topic associated with the idea.
    /// </summary>
    [Column("topicId")]
    public required Guid TopicId { get; set; }
}
