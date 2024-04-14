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
public class TopicService(IRepository<Topic> topicRepository) : IService<Topic>
{
    private IRepository<Topic> topicRepository = topicRepository;

    /// <inheritdoc/>
    public List<Topic> GetAll()
    {
        List<Topic> topicList = this.topicRepository.GetAll().ToList();
        return topicList;
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
    public List<Topic> FilterByGivenTopics(string filter, string keyword)
    {
        if (filter.ToLower() != "id" && filter.ToLower() != "title")
            {
                throw new Exception("Error 404: Not Found");
            }

        if (filter.ToLower() == "id")
            {
                return FilterById(keyword.ToLower());
            }
            else if (filter.ToLower() == "title")
            {
                return FilterByTitle(keyword.ToLower());
            }
            else
            {
                throw new Exception("Error 404: Not Found");
            }
    }

    /// <inheritdoc/>
    public List<Topic> FilterById(string keyword)
    {
        List<Topic> filteredTopics = this.topicRepository.GetAll()
                .Where(topic => topic.Id.ToString().ToLower().Contains(keyword))
                .ToList();
        return filteredTopics;
    }

    /// <inheritdoc/>
    public List<Topic> FilterByTitle(string keyword)
    {
        List<Topic> filteredTopics = this.topicRepository.GetAll()
                .Where(topic => topic.Title.ToLower().Contains(keyword))
                .ToList();
        return filteredTopics;
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
