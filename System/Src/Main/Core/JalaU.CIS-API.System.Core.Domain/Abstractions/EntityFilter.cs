//-----------------------------------------------------------------------
// <copyright file="EntityFilter.cs" company="Jala University">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace JalaU.CIS_API.System.Core.Domain;

/// <summary>
/// Abstract class for filtering entities.
/// </summary>
/// <typeparam name="T">The type of entity handled by the repository.</typeparam>
public abstract class EntityFilter<T>
{
    /// <summary>
    /// Gets or sets the list of entities.
    /// </summary>
     protected List<T> Entities { get; set; } = [];

    /// <summary>
    /// Filters a list of entities based on a specified filter and keyword.
    /// </summary>
    /// <param name="entitiesToFilter">The list of entities to filter.</param>
    /// <param name="filter">The filter to apply.</param>
    /// <param name="keyword">The keyword to use for filtering.</param>
    /// <returns>A filtered list of entities.</returns>
     public abstract List<T> Filter(List<T> entitiesToFilter, string filter, string keyword);
}
