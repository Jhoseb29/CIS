//-----------------------------------------------------------------------
// <copyright file="VoteRepository.cs" company="Jala University">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using JalaU.CIS_API.System.Core.Domain;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace JalaU.CIS_API.System.Data.MongoDBPersistance;

/// <summary>
/// Initializes a new instance of the <see cref="VoteRepository"/> class.
/// </summary>
/// <param name="appDbContext">The database context used for database operations.</param>
public class VoteRepository(MongoDbContext appDbContext) : IRepository<Vote>
{
    private readonly MongoDbContext appDbContext = appDbContext;

    /// <inheritdoc/>
    public IEnumerable<Vote> GetAll()
    {
        var votes = this.appDbContext.topics.SelectMany(topic => topic.Ideas)
            .SelectMany(idea => idea.Votes)
            .ToList();
        return votes;
    }

    /// <inheritdoc/>
    public Vote Save(Vote entity)
    {
        var topic = this.appDbContext.topics.FirstOrDefault(
            t => t.Ideas.Any(idea => idea.Id == entity.IdeaId)
        );

        var idea = topic!.Ideas.FirstOrDefault(idea => idea.Id == entity.IdeaId);

        idea!.Votes.Add(entity);
        this.appDbContext.SaveChanges();

        return entity;
    }

    /// <inheritdoc/>
    public Vote Update(Vote entity)
    {
        var topic = this.appDbContext.topics.FirstOrDefault(
            t => t.Ideas.Any(idea => idea.Id == entity.IdeaId)
        );

        var idea = topic!.Ideas.FirstOrDefault(idea => idea.Id == entity.IdeaId);
        var vote = idea!.Votes.FirstOrDefault(vote => vote.Id == entity.Id);

        vote = entity;
        this.appDbContext.SaveChanges();

        return entity;
    }

    /// <inheritdoc/>
    public Vote Delete(Vote entity)
    {
        var topic = this.appDbContext.topics.FirstOrDefault(
            t => t.Ideas.Any(idea => idea.Id == entity.IdeaId)
        );

        var idea = topic!.Ideas.FirstOrDefault(idea => idea.Id == entity.IdeaId);
        idea!.Votes.Remove(entity);
        this.appDbContext.SaveChanges();

        return entity;
    }

    /// <inheritdoc/>
    public Vote? GetByCriteria(Func<Vote, bool> criteria)
    {
        var votes = this.GetAll();
        Vote? vote = votes.FirstOrDefault(criteria);
        return vote;
    }
}
