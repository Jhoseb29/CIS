//-----------------------------------------------------------------------
// <copyright file="Topic.cs" company="Jala University">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JalaU.CIS_API.System.Core.Domain;

/// <summary>
/// Represents a topic entity.
/// </summary>
public class Topic
{
    /// <summary>
    /// Gets or sets the unique identifier of the topic.
    /// </summary>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public required Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the title of the topic.
    /// </summary>
    [Column("title")]
    [StringLength(200)]
    public required string Title { get; set; }

    /// <summary>
    /// Gets or sets the description of the topic.
    /// </summary>
    [Column("description")]
    [StringLength(500)]
    public required string Description { get; set; }

    /// <summary>
    /// Gets or sets the date when the topic was created.
    /// </summary>
    [Column("date")]
    public required DateTime Date { get; set; } = DateTime.Now;

    /// <summary>
    /// Gets or sets the labels associated with the topic.
    /// </summary>
    [Column("labels")]
    [MinLength(1, ErrorMessage = "There must be at leat one label for the topic.")]
    public required List<string> Labels { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the user who created the topic.
    /// </summary>
    [Column("userId")]
    public required Guid UserId { get; set; }
}
