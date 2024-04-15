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
    public Topic GetById(Guid guid)
    {
        Topic topic = this.topicRepository.GetById(guid);
        return topic;
    }

    /// <inheritdoc/>
    public Topic GetByTitle(string title)
    {
        Topic topic = this.topicRepository.GetByTitle(title);
        return topic;
    }

    /// <inheritdoc/>
    public Topic? Save(Topic TopicToSave)
    {
        ValidatorCreateTopic validator = new ValidatorCreateTopic();
        if (validator.AreThereAnyBadWord(TopicToSave) & validator.NullableFields(TopicToSave).Count != 0)
        {
            return null;
        }
        else
        {
            Topic topic = this.topicRepository.Save(TopicToSave);
            return topic;
        }
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
