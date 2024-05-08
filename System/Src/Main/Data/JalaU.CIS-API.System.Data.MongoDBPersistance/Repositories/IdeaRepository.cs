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
        List<Idea> ideasList = [.. this.appDbContext.ideas];
        return ideasList;
    }

    /// <inheritdoc/>
    public Idea Save(Idea entity)
    {
        this.appDbContext.Add(entity);
        this.appDbContext.SaveChanges();
        return entity;
    }

    /// <inheritdoc/>
    public Idea Update(Idea entity)
    {
        this.appDbContext.Update(entity);
        this.appDbContext.SaveChanges();
        return entity;
    }

    /// <inheritdoc/>
    public Idea Delete(Idea entity)
    {
        var votes = this.appDbContext.votes.Where(vote => vote.IdeaId == entity.Id);
        this.appDbContext.votes.RemoveRange(votes);
        this.appDbContext.ideas.Remove(entity);

        this.appDbContext.SaveChanges();

        return entity;
    }

    /// <inheritdoc/>
    public Idea? GetByCriteria(Func<Idea, bool> criteria)
    {
        Idea? idea = this.appDbContext.ideas.FirstOrDefault(criteria);
        return idea;
    }
}
