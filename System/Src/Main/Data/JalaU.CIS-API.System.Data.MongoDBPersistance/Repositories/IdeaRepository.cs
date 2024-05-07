//-----------------------------------------------------------------------
// <copyright file="IdeaRepository.cs" company="Jala University">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using JalaU.CIS_API.System.Core.Domain;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace JalaU.CIS_API.System.Data.MongoDBPersistance;

/// <summary>
/// Initializes a new instance of the <see cref="IdeaRepository"/> class.
/// </summary>
/// <param name="appDbContext">The database context used for database operations.</param>
public class IdeaRepository(MongoDbContext appDbContext) : IRepository<Idea>
{
    private readonly MongoDbContext appDbContext = appDbContext;

    /// <inheritdoc/>
    public IEnumerable<Idea> GetAll()
    {
        var ideasList = this.appDbContext.topics.SelectMany(topic => topic.Ideas).ToList();
        return ideasList;
    }

    /// <inheritdoc/>
    public Idea Save(Idea entity)
    {
        var topic = this.appDbContext.topics.FirstOrDefault(t => t.Id == entity.TopicId);

        topic!.Ideas.Add(entity);
        this.appDbContext.SaveChanges();

        return entity;
    }

    /// <inheritdoc/>
    public Idea Update(Idea entity)
    {
        var topic = this.appDbContext.topics.FirstOrDefault(
            t => t.Ideas.Any(i => i.Id == entity.Id)
        );

        var existingIdea = topic!.Ideas.FirstOrDefault(i => i.Id == entity.Id);

        existingIdea = entity;
        this.appDbContext.SaveChanges();

        return entity;
    }

    /// <inheritdoc/>
    public Idea Delete(Idea entity)
    {
        var topic = this.appDbContext.topics.FirstOrDefault(
            t => t.Ideas.Any(i => i.Id == entity.Id)
        );
        topic!.Ideas.Remove(entity);
        this.appDbContext.SaveChanges();

        return entity;
    }

    /// <inheritdoc/>
    public Idea? GetByCriteria(Func<Idea, bool> criteria)
    {
        var ideasList = this.GetAll();
        Idea? idea = ideasList.FirstOrDefault(criteria);
        return idea;
    }
}
