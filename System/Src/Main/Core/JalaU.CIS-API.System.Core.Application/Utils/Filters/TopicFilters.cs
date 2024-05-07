//-----------------------------------------------------------------------
// <copyright file="TopicFilters.cs" company="Jala University">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace JalaU.CIS_API.System.Core.Application;

using global::System.Net;
using JalaU.CIS_API.System.Core.Domain;

/// <summary>
/// Utility class for calling filters on topics list.
/// </summary>
public class TopicFilters : EntityFilter<Topic>
{
    /// <inheritdoc/>
    public override List<Topic> Filter(List<Topic> topics, string filter, string keyword)
    {
        this.Entities = topics;
        return filter.ToLower() switch
        {
            "id" => this.FilterById(keyword.ToLower()),
            "title" => this.FilterByTitle(keyword.ToLower()),
            "description" => this.FilterByDescription(keyword.ToLower()),
            "labels" => this.FilterByLabels(keyword.ToLower()),
            "userid" => this.FilterByUserId(keyword.ToLower()),
            _
                => throw new WrongDataException(
                    "errors",
                    [
                        new(
                            (int)HttpStatusCode.UnprocessableContent,
                            $"The filter {filter} can't be used."
                        )
                    ]
                ),
        };
    }

    /// <summary>
    /// Filter the Topics by id.
    /// </summary>
    /// <param name="keyword">The key word to apply the filter.</param>
    /// <returns>A list of entities with the characteristics asked.</returns>
    ///
    private List<Topic> FilterById(string keyword)
    {
        List<Topic> filteredTopics = this.Entities.Where(
            topic =>
                topic.Id.ToString().Contains(keyword, StringComparison.CurrentCultureIgnoreCase)
        )
            .ToList();
        return filteredTopics;
    }

    /// <summary>
    /// Filter the Topics by title.
    /// </summary>
    /// <param name="keyword">The key word to apply the filter.</param>
    /// <returns>A list of entities with the characteristics asked.</returns>
    ///
    private List<Topic> FilterByTitle(string keyword)
    {
        List<Topic> filteredTopics = this.Entities.Where(
            topic => topic.Title.ToLower().Contains(keyword)
        )
            .ToList();
        return filteredTopics;
    }

    /// <summary>
    /// Filter the Topics by description.
    /// </summary>
    /// <param name="keyword">The key word to apply the filter.</param>
    /// <returns>A list of entities with the characteristics asked.</returns>
    ///
    private List<Topic> FilterByDescription(string keyword)
    {
        List<Topic> filteredTopics = this.Entities.Where(
            topic => topic.Description.ToLower().Contains(keyword)
        )
            .ToList();
        return filteredTopics;
    }

    /// <summary>
    /// Filter the Topics by labels.
    /// </summary>
    /// <param name="keyword">The key word to apply the filter.</param>
    /// <returns>A list of entities with the characteristics asked.</returns>
    ///
    private List<Topic> FilterByLabels(string keyword)
    {
        List<Topic> filteredTopics = this.Entities.Where(
            topic =>
                topic.Labels.Any(
                    label => label.Contains(keyword, StringComparison.CurrentCultureIgnoreCase)
                )
        )
            .ToList();
        return filteredTopics;
    }

    /// <summary>
    /// Filter the Topics by user id.
    /// </summary>
    /// <param name="keyword">The key word to apply the filter.</param>
    /// <returns>A list of entities with the characteristics asked.</returns>
    ///
    private List<Topic> FilterByUserId(string keyword)
    {
        List<Topic> filteredTopics = this.Entities.Where(
            topic => topic.UserId.ToString().ToLower().Contains(keyword)
        )
            .ToList();
        return filteredTopics;
    }
}
