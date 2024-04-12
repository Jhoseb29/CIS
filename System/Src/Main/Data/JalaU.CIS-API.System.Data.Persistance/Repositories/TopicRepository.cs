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
    private AppDbContext appDbContext = appDbContext;

    /// <inheritdoc/>
    public IEnumerable<Topic> GetAll()
    {
        List<Topic> topicList = this.appDbContext.topics.ToList();
        return topicList;
    }

    /// <inheritdoc/>
    public Topic GetById(Guid id)
    {
        Topic topic = this.appDbContext.topics.FirstOrDefault(t => t.Id == id);
        return topic;
    }

    /// <inheritdoc/>
    public Topic GetByTitle(string title)
    {
        Topic topic = this.appDbContext.topics.FirstOrDefault(t => t.Title == title);
        return topic;
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
