﻿//-----------------------------------------------------------------------
// <copyright file="TopicRepository.cs" company="Jala University">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace JalaU.CIS_API.System.Data.MongoDBPersistance;

using JalaU.CIS_API.System.Core.Domain;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Initializes a new instance of the <see cref="TopicRepository"/> class.
/// </summary>
/// <param name="appDbContext">The database context used for database operations.</param>
public class TopicRepository(MongoDbContext appDbContext) : IRepository<Topic>
{
    private readonly MongoDbContext appDbContext = appDbContext;

    /// <inheritdoc/>
    public IEnumerable<Topic> GetAll()
    {
        List<Topic> topicList = [.. this.appDbContext.topics];
        return topicList;
    }

    /// <inheritdoc/>
    public Topic Save(Topic entity)
    {
        this.appDbContext.Add(entity);
        this.appDbContext.SaveChanges();
        return entity;
    }

    /// <inheritdoc/>
    public Topic Update(Topic entity)
    {
        this.appDbContext.Update(entity);
        this.appDbContext.SaveChanges();
        return entity;
    }

    /// <inheritdoc/>
    public Topic Delete(Topic entity)
    {
        var ideas = this.appDbContext.ideas.Where(idea => idea.TopicId == entity.Id);
        foreach (var idea in ideas)
        {
            this.appDbContext.votes.RemoveRange(
                this.appDbContext.votes.Where(vote => vote.IdeaId == idea.Id)
            );
        }
        this.appDbContext.ideas.RemoveRange(ideas);

        this.appDbContext.topics.Remove(entity);

        this.appDbContext.SaveChanges();

        return entity;
    }

    /// <inheritdoc/>
    public Topic? GetByCriteria(Func<Topic, bool> criteria)
    {
        Topic? topic = this.appDbContext.topics.FirstOrDefault(criteria);
        return topic;
    }
}