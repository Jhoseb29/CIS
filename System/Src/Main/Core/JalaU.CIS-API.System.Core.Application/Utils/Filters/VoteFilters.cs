﻿//-----------------------------------------------------------------------
// <copyright file="VoteFilters.cs" company="Jala University">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

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
        filter = filter.ToLower();
        switch (filter)
        {
            default:
                return this.Entities;
        }
    }
}