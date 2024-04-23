//-----------------------------------------------------------------------
// <copyright file="VoteRepository.cs" company="Jala University">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace JalaU.CIS_API.System.Data.Persistance;

using JalaU.CIS_API.System.Core.Domain;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Initializes a new instance of the <see cref="VoteRepository"/> class.
/// </summary>
/// <param name="appDbContext">The database context used for database operations.</param>
public class VoteRepository(AppDbContext appDbContext) : IRepository<Vote>
{
    private readonly AppDbContext appDbContext = appDbContext;

    /// <inheritdoc/>
    public IEnumerable<Vote> GetAll()
    {
        List<Vote> votesList = [.. this.appDbContext.votes];
        return votesList;
    }

    /// <inheritdoc/>
    public Vote Save(Vote entity)
    {
        this.appDbContext.Add(entity);
        this.appDbContext.SaveChanges();
        return entity;
    }

    /// <inheritdoc/>
    public Vote Update(Vote entity)
    {
        this.appDbContext.Update(entity);
        this.appDbContext.SaveChanges();
        return entity;
    }

    /// <inheritdoc/>
    public Vote Delete(Vote entity)
    {
        this.appDbContext.votes.Remove(entity);

        this.appDbContext.SaveChanges();

        return entity;
    }

    /// <inheritdoc/>
    public Vote? GetByCriteria(Func<Vote, bool> criteria)
    {
        Vote? vote = this.appDbContext.votes.FirstOrDefault(criteria);
        return vote;
    }
}
