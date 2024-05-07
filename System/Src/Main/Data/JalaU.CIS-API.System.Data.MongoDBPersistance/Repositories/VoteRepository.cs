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
        var votes = this.appDbContext.topics.AsQueryable()
            .SelectMany(t => t.Ideas)
            .SelectMany(i => i.Votes);
        return [.. votes];
    }

    /// <inheritdoc/>
    public Vote Save(Vote entity)
    {
        var topic = this.appDbContext.topics.FirstOrDefault(
            t => t.Ideas.Any(i => i.Id == entity.IdeaId)
        );

        if (topic != null)
        {
            var idea = topic.Ideas.FirstOrDefault(i => i.Id == entity.IdeaId);
            if (idea != null)
            {
                idea.Votes.Add(entity);
                this.appDbContext.SaveChanges();
                return entity;
            }
        }
        return null!;
    }

    /// <inheritdoc/>
    public Vote Update(Vote entity)
    {
        var topic = this.appDbContext.topics.FirstOrDefault(
            t => t.Ideas.Any(i => i.Votes.Any(v => v.Id == entity.Id))
        );

        if (topic != null)
        {
            var ideaToUpdate = topic.Ideas.FirstOrDefault(i => i.Votes.Any(v => v.Id == entity.Id));
            if (ideaToUpdate != null)
            {
                var voteToUpdate = ideaToUpdate.Votes.FirstOrDefault(v => v.Id == entity.Id);
                if (voteToUpdate != null)
                {
                    this.appDbContext.Entry(voteToUpdate).CurrentValues.SetValues(entity);
                    this.appDbContext.SaveChanges();

                    return entity;
                }
            }
        }
        return null!;
    }

    /// <inheritdoc/>
    public Vote Delete(Vote entity)
    {
        var topic = this.appDbContext.topics.FirstOrDefault(
            t => t.Ideas.Any(i => i.Votes.Any(v => v.Id == entity.Id))
        );

        if (topic != null)
        {
            var idea = topic.Ideas.FirstOrDefault(i => i.Votes.Any(v => v.Id == entity.Id));
            if (idea != null)
            {
                var voteToDelete = idea.Votes.FirstOrDefault(v => v.Id == entity.Id);
                if (voteToDelete != null)
                {
                    idea.Votes.Remove(voteToDelete);
                    this.appDbContext.SaveChanges();
                    return voteToDelete;
                }
            }
        }

        return null!;
    }

    /// <inheritdoc/>
    public Vote GetByCriteria(Func<Vote, bool> criteria)
    {
        var vote = this.appDbContext.topics.AsQueryable()
            .SelectMany(t => t.Ideas)
            .SelectMany(i => i.Votes)
            .FirstOrDefault(criteria);
        return vote!;
    }
}
