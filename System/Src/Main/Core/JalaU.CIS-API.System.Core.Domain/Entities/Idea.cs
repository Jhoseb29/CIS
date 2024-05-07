//-----------------------------------------------------------------------
// <copyright file="Idea.cs" company="Jala University">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace JalaU.CIS_API.System.Core.Domain;

/// <summary>
/// Represents an idea entity.
/// </summary>
public class Idea
{
    /// <summary>
    /// Gets or sets the unique identifier of the idea.
    /// </summary>
    [BsonId]
    [BsonElement("_id")]
    public required Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the title of the idea.
    /// </summary>
    [BsonElement("title")]
    public required string Title { get; set; }

    /// <summary>
    /// Gets or sets the description of the idea.
    /// </summary>
    [BsonElement("description")]
    public required string Description { get; set; }

    /// <summary>
    /// Gets or sets the date when the idea was created.
    /// </summary>
    [BsonElement("date")]
    public required DateTime Date { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the topic to which this idea belongs.
    /// </summary>
    [BsonElement("topicId")]
    public required Guid TopicId { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the user who created the idea.
    /// </summary>
    [BsonElement("userId")]
    public required Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the ideas associated with the topic.
    /// </summary>
    [BsonElement("votes")]
    public required List<Vote> Votes { get; set; }
}
