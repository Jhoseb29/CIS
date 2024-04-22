//-----------------------------------------------------------------------
// <copyright file="IdeaRepository.cs" company="Jala University">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace JalaU.CIS_API.System.Data.Persistance;

using JalaU.CIS_API.System.Core.Domain;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Initializes a new instance of the <see cref="IdeaRepository"/> class.
/// </summary>
/// <param name="appDbContext">The database context used for database operations.</param>
public class IdeaRepository(AppDbContext appDbContext) : IRepository<Idea>
{
    private readonly AppDbContext appDbContext = appDbContext;

    /// <inheritdoc/>
    public IEnumerable<Idea> GetAll()
    {
        List<Idea> ideaList = [.. this.appDbContext.ideas];
        return ideaList;
    }

    /// <inheritdoc/>
    public Idea Save(Idea entity)
    {
        this.appDbContext.Add(entity);
        this.appDbContext.Update(entity);
        this.appDbContext.SaveChanges();
        return entity;
    }

    /// <inheritdoc/>
    public Idea Update(Idea entity)
    {
        try
        {
            this.appDbContext.Update(entity);
            this.appDbContext.SaveChanges();
            return entity;
        }
        catch (DbUpdateException)
        {
            throw new DuplicateEntryException("The Idea's title already exists in the System.");
        }
    }

    /// <inheritdoc/>
    public Idea Delete(Idea entity)
    {
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
