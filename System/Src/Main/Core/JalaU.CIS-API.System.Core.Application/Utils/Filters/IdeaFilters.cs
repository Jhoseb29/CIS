//-----------------------------------------------------------------------
// <copyright file="IdeaFilters.cs" company="Jala University">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace JalaU.CIS_API.System.Core.Application;

using global::System.Net;
using JalaU.CIS_API.System.Core.Domain;

/// <summary>
/// Utility class for calling filters on ideas list.
/// </summary>
public class IdeaFilters : EntityFilter<Idea>
{
    /// <inheritdoc/>
    public override List<Idea> Filter(List<Idea> ideas, string filter, string keyword)
    {
        this.Entities = ideas;
        return filter.ToLower() switch
        {
            "id" => this.FilterById(keyword.ToLower()),
            "title" => this.FilterByTitle(keyword.ToLower()),
            "description" => this.FilterByDescription(keyword.ToLower()),
            "topicid" => this.FilterBytopicId(keyword.ToLower()),
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
    /// Filter the Idea by id.
    /// </summary>
    /// <param name="keyword">The key word to apply the filter.</param>
    /// <returns>A list of entities with the characteristics asked.</returns>
    ///
    private List<Idea> FilterById(string keyword)
    {
        List<Idea> filteredIdeas = this.Entities.Where(
            idea =>
                idea.Id.ToString().Contains(keyword, StringComparison.CurrentCultureIgnoreCase)
        )
            .ToList();
        return filteredIdeas;
    }

    /// <summary>
    /// Filter the Idea by title.
    /// </summary>
    /// <param name="keyword">The key word to apply the filter.</param>
    /// <returns>A list of entities with the characteristics asked.</returns>
    ///
    private List<Idea> FilterByTitle(string keyword)
    {
        List<Idea> filteredIdeas = this.Entities.Where(
            idea => idea.Title.ToLower().Contains(keyword)
        )
            .ToList();
        return filteredIdeas;
    }

    /// <summary>
    /// Filter the Idea by description.
    /// </summary>
    /// <param name="keyword">The key word to apply the filter.</param>
    /// <returns>A list of entities with the characteristics asked.</returns>
    ///
    private List<Idea> FilterByDescription(string keyword)
    {
        List<Idea> filteredIdeas = this.Entities.Where(
            idea => idea.Description.ToLower().Contains(keyword)
        )
            .ToList();
        return filteredIdeas;
    }

    /// <summary>
    /// Filter the Idea by user id.
    /// </summary>
    /// <param name="keyword">The key word to apply the filter.</param>
    /// <returns>A list of entities with the characteristics asked.</returns>
    ///
    private List<Idea> FilterByUserId(string keyword)
    {
        List<Idea> filteredIdeas = this.Entities.Where(
            idea => idea.UserId.ToString().ToLower().Contains(keyword)
        )
            .ToList();
        return filteredIdeas;
    }

    /// <summary>
    /// Filter the Idea by topic id.
    /// </summary>
    /// <param name="keyword">The key word to apply the filter.</param>
    /// <returns>A list of entities with the characteristics asked.</returns>
    ///
    private List<Idea> FilterBytopicId(string keyword)
    {
        List<Idea> filteredIdea = this.Entities.Where(
            idea => idea.TopicId.ToString().ToLower().Contains(keyword)
        )
            .ToList();
        return filteredIdea;
    }
}
