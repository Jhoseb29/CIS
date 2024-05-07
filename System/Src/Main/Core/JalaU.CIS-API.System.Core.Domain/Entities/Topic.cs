//-----------------------------------------------------------------------
// <copyright file="Topic.cs" company="Jala University">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace JalaU.CIS_API.System.Core.Domain;

/// <summary>
/// Represents a topic entity.
/// </summary>
public class Topic
{
    /// <summary>
    /// Gets or sets the unique identifier of the topic.
    /// </summary>
    [BsonId]
    [BsonElement("_id")]
    public required Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the title of the topic.
    /// </summary>
    [BsonElement("title")]
    public required string Title { get; set; }

    /// <summary>
    /// Gets or sets the description of the topic.
    /// </summary>
    [BsonElement("description")]
    public required string Description { get; set; }

    /// <summary>
    /// Gets or sets the date when the topic was created.
    /// </summary>
    [BsonElement("date")]
    public required DateTime Date { get; set; }

    /// <summary>
    /// Gets or sets the labels associated with the topic.
    /// </summary>
    [BsonElement("labels")]
    public required List<string> Labels { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the user who created the topic.
    /// </summary>
    [BsonElement("userId")]
    public required Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the ideas associated with the topic.
    /// </summary>
    [BsonElement("ideas")]
    public required List<Idea> Ideas { get; set; }
}
