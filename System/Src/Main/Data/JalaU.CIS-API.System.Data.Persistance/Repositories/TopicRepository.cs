//-----------------------------------------------------------------------
// <copyright file="TopicRepository.cs" company="Jala University">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace JalaU.CIS_API.System.Data.Persistance;

using JalaU.CIS_API.System.Core.Domain;

/// <summary>
/// Initializes a new instance of the <see cref="TopicRepository"/> class.
/// </summary>
/// <param name="appDbContext">The database context used for database operations.</param>
public class TopicRepository(AppDbContext appDbContext) : IRepository<Topic>
{
    private readonly AppDbContext appDbContext = appDbContext;

    /// <inheritdoc/>
    public IEnumerable<Topic> GetAll()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Topic GetById(Guid id)
    {
        throw new NotImplementedException();
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
        this.appDbContext.Update(topic);
        this.appDbContext.SaveChanges();
        return topic;
    }

    /// <inheritdoc/>
    public Topic Delete(Topic entity)
    {
        throw new NotImplementedException();
    }
}
