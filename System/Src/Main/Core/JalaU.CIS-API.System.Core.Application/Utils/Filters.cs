//-----------------------------------------------------------------------
// <copyright file="Filters.cs" company="Jala University">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace JalaU.CIS_API.System.Core.Application;

using JalaU.CIS_API.System.Core.Domain;

/// <summary>
/// Utility class for calling filters on topics list.
/// </summary>
public class Filters(IRepository<Topic> topicRepository)
{
    private IRepository<Topic> topicRepository = topicRepository;

    /// <summary>
    /// Filter the Topics by id.
    /// </summary>
    /// <param name="keyword">The key word to apply the filter.</param>
    /// <returns>A list of entities with the characteristics asked.</returns>
    ///
    public List<Topic> FilterById(string keyword)
    {
        List<Topic> filteredTopics = this.topicRepository.GetAll()
                .Where(topic => topic.Id.ToString().ToLower().Contains(keyword))
                .ToList();
        return filteredTopics;
    }

    /// <summary>
    /// Filter the Topics by title.
    /// </summary>
    /// <param name="keyword">The key word to apply the filter.</param>
    /// <returns>A list of entities with the characteristics asked.</returns>
    ///
    public List<Topic> FilterByTitle(string keyword)
    {
        List<Topic> filteredTopics = this.topicRepository.GetAll()
                .Where(topic => topic.Title.ToLower().Contains(keyword))
                .ToList();
        return filteredTopics;
    }

    /// <summary>
    /// Filter the Topics by description.
    /// </summary>
    /// <param name="keyword">The key word to apply the filter.</param>
    /// <returns>A list of entities with the characteristics asked.</returns>
    ///
    public List<Topic> FilterByDescription(string keyword)
    {
        List<Topic> filteredTopics = this.topicRepository.GetAll()
                .Where(topic => topic.Description.ToLower().Contains(keyword))
                .ToList();
        return filteredTopics;
    }

    /// <summary>
    /// Filter the Topics by labels.
    /// </summary>
    /// <param name="keyword">The key word to apply the filter.</param>
    /// <returns>A list of entities with the characteristics asked.</returns>
    ///
    public List<Topic> FilterByLabels(string keyword)
    {
        List<Topic> filteredTopics = this.topicRepository.GetAll()
            .Where(topic => topic.Labels.Any(label => label.ToLower().Contains(keyword.ToLower())))
            .ToList();
        return filteredTopics;
    }

    /// <summary>
    /// Filter the Topics by user id.
    /// </summary>
    /// <param name="keyword">The key word to apply the filter.</param>
    /// <returns>A list of entities with the characteristics asked.</returns>
    ///
    public List<Topic> FilterByUserId(string keyword)
    {
        List<Topic> filteredTopics = this.topicRepository.GetAll()
            .Where(topic => topic.UserId.ToString().ToLower().Contains(keyword))
            .ToList();
        return filteredTopics;
    }
}