﻿//-----------------------------------------------------------------------
// <copyright file="TopicRepository.cs" company="Jala University">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace JalaU.CIS_API.System.Data.Persistance;

using JalaU.CIS_API.System.Core.Domain;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Initializes a new instance of the <see cref="TopicRepository"/> class.
/// </summary>
/// <param name="appDbContext">The database context used for database operations.</param>
public class TopicRepository(AppDbContext appDbContext) : IRepository<Topic>
{
    private AppDbContext appDbContext = appDbContext;

    /// <inheritdoc/>
    public IEnumerable<Topic> GetAll()
    {
        List<Topic> topicList = this.appDbContext.topics.ToList();
        return topicList;
    }

    /// <inheritdoc/>
    public Topic Save(Topic entity)
    {
        this.appDbContext.Add(entity);
        this.appDbContext.Update(entity);
        this.appDbContext.SaveChanges();
        return entity;
    }

    /// <inheritdoc/>
    public Topic Update(Topic topic)
    {
        try
        {
            this.appDbContext.Update(topic);
            this.appDbContext.SaveChanges();
            return topic;
        }
        catch (DbUpdateException)
        {
            throw new DuplicateEntryException("The Topic's title already exists in the System.");
        }
    }

    /// <inheritdoc/>
    public Topic Delete(Topic entity)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Topic? GetByCriteria(Func<Topic, bool> criteria)
    {
        Topic? topic = this.appDbContext.topics.FirstOrDefault(criteria);
        return topic;
    }
}
