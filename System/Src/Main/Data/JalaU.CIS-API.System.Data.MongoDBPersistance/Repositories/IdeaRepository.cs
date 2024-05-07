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
        List<Idea>? ideas = [.. this.appDbContext.topics.AsQueryable().SelectMany(t => t.Ideas)];
        return ideas;
    }

    /// <inheritdoc/>
    public Idea Save(Idea entity)
    {
        var topic = this.appDbContext.topics.FirstOrDefault(t => t.Id == entity.TopicId);

        if (topic != null)
        {
            topic.Ideas.Add(entity);
            this.appDbContext.SaveChanges();
            return entity;
        }

        return null!;
    }

    /// <inheritdoc/>
    public Idea Update(Idea entity)
    {
        var topic = this.appDbContext.topics.Include(t => t.Ideas)
            .FirstOrDefault(t => t.Id == entity.TopicId);

        if (topic != null)
        {
            var ideaToUpdate = topic.Ideas.FirstOrDefault(i => i.Id == entity.Id);
            if (ideaToUpdate != null)
            {
                this.appDbContext.Entry(ideaToUpdate).CurrentValues.SetValues(entity);
                this.appDbContext.SaveChanges();
                return entity;
            }
        }

        return null!;
    }

    /// <inheritdoc/>
    public Idea Delete(Idea entity)
    {
        var topic = this.appDbContext.topics.Include(t => t.Ideas)
            .FirstOrDefault(t => t.Id == entity.TopicId);

        if (topic != null)
        {
            var ideaToDelete = topic.Ideas.FirstOrDefault(i => i.Id == entity.Id);
            if (ideaToDelete != null)
            {
                topic.Ideas.Remove(ideaToDelete);
                this.appDbContext.SaveChanges();
                return ideaToDelete;
            }
        }

        return null!;
    }

    /// <inheritdoc/>
    public Idea GetByCriteria(Func<Idea, bool> criteria)
    {
        var idea = this.appDbContext.topics.AsQueryable()
            .SelectMany(t => t.Ideas)
            .FirstOrDefault(criteria);
        return idea!;
    }
}
