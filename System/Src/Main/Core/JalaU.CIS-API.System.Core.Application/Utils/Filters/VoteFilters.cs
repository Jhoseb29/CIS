//-----------------------------------------------------------------------
// <copyright file="VoteFilters.cs" company="Jala University">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System.Net;

namespace JalaU.CIS_API.System.Core.Application;

using JalaU.CIS_API.System.Core.Domain;

/// <summary>
/// Utility class for calling filters on votes list.
/// </summary>
public class VoteFilters : EntityFilter<Vote>
{
    /// <inheritdoc/>
    public override List<Vote> Filter(List<Vote> votes, string filter, string keyword)
    {
        this.Entities = votes;
        return filter.ToLower() switch
        {
            "id" => this.FilterById(keyword.ToLower()),
            "positive" => this.FilterByPositive(keyword.ToLower()),
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

    private List<Vote> FilterById(string keyword)
    {
        List<Vote> filteredVotes = this.Entities.Where(
                vote =>
                    vote.Id.ToString().Contains(keyword, StringComparison.CurrentCultureIgnoreCase)
            )
            .ToList();
        return filteredVotes;
    }

    private List<Vote> FilterByPositive(string keyword)
    {
        bool isPositive = keyword.ToLower() == "true";
        List<Vote> filteredVotes = this.Entities.Where(vote => vote.Positive == isPositive).ToList();
        return filteredVotes;
    }

    private List<Vote> FilterByUserId(string keyword)
    {
        List<Vote> filteredTopics = this.Entities.Where(
                vote => vote.UserId.ToString().ToLower().Contains(keyword)
            )
            .ToList();
        return filteredTopics;
    }
}