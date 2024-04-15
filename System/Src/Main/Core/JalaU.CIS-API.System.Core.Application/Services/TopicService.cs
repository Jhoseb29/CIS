//-----------------------------------------------------------------------
// <copyright file="TopicService.cs" company="Jala University">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace JalaU.CIS_API.System.Core.Application;

using JalaU.CIS_API.System.Core.Domain;

/// <summary>
/// Represents a service for managing topics.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="TopicService"/> class.
/// </remarks>
/// <param name="topicRepository">The repository for topics.</param>
public class TopicService(IRepository<Topic> topicRepository, EntityFilter<Topic> entityFilter)
    : IService<Topic>
{
    private readonly EntityFilter<Topic> filters = entityFilter;
    private IRepository<Topic> topicRepository = topicRepository;

    /// <inheritdoc/>
    public List<Topic> GetAll()
    {
        List<Topic> topicList = this.topicRepository.GetAll().ToList();
        return topicList;
    }

    /// <inheritdoc/>
    public List<Topic> FilterEntities(string filter, string keyword)
    {
        return this.filters.Filter(this.GetAll(), filter, keyword);
    }

    /// <inheritdoc/>
    public Topic? GetById(Guid id)
    {
        return this.topicRepository.GetByCriteria(t => t.Id == id);
    }

    /// <inheritdoc/>
    private Topic? GetByTitle(string title)
    {
        return this.topicRepository.GetByCriteria(t => t.Title == title);
    }

    /// <inheritdoc/>
    public Topic? GetByCriteria(string field, string valueToSearch)
    {
        switch (field.ToLower())
        {
            case "id":
                return this.GetById(Guid.Parse(valueToSearch));
            case "title":
                return this.GetByTitle(valueToSearch);
            default:
                throw new ArgumentException("Invalid field.");
        }
    }

    /// <inheritdoc/>
    public Topic Save(BaseRequestDTO entityToSave)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Topic Update(BaseRequestDTO entityToSave, string id)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Topic DeleteById(Guid guid)
    {
        throw new NotImplementedException();
    }
}
