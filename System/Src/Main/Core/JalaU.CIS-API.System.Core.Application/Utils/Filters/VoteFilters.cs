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
        throw new NotImplementedException();
    }

    private List<Vote> FilterByPositive(string keyword)
    {
        throw new NotImplementedException();
    }

    private List<Vote> FilterByUserId(string keyword)
    {
        throw new NotImplementedException();
    }
}