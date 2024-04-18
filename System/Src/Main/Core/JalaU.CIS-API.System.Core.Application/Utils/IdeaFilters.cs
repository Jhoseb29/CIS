//-----------------------------------------------------------------------
// <copyright file="IdeaFilters.cs" company="Jala University">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace JalaU.CIS_API.System.Core.Application;

using JalaU.CIS_API.System.Core.Domain;

/// <summary>
/// Utility class for calling filters on topics list.
/// </summary>
public class IdeaFilters : EntityFilter<Idea>
{
    /// <inheritdoc/>
    public override List<Idea> Filter(List<Idea> entitiesToFilter, string filter, string keyword)
    {
        throw new NotImplementedException();
    }
}
