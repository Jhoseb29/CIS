//-----------------------------------------------------------------------
// <copyright file="Vote.cs" company="Jala University">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace JalaU.CIS_API.System.Core.Domain;

/// <summary>
/// Represents a vote entity.
/// </summary>
public class Vote
{
    /// <summary>
    /// Gets or sets the unique identifier of the idea.
    /// </summary>
    [BsonId]
    [BsonElement("_id")]
    public required string Id { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// Gets or sets a value indicating whether gets or sets the title of the idea.
    /// </summary>
    [BsonElement("positive")]
    public required bool Positive { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the idea to which this vote belongs.
    /// </summary>
    [BsonElement("ideaId")]
    public required string IdeaId { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the user who created the idea.
    /// </summary>
    [BsonElement("userId")]
    public required string UserId { get; set; }
}
