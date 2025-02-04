﻿//-----------------------------------------------------------------------
// <copyright file="IRepository.cs" company="Jala University">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace JalaU.CIS_API.System.Core.Domain;

/// <summary>
/// Represents a generic repository interface.
/// </summary>
/// <typeparam name="T">The type of entity handled by the repository.</typeparam>
public interface IRepository<T>
{
    /// <summary>
    /// Retrieves all entities from the repository.
    /// </summary>
    /// <returns>An enumerable collection of entities.</returns>
    IEnumerable<T> GetAll();

    /// <summary>
    /// Saves an entity to the repository.
    /// </summary>
    /// <param name="entity">The entity to be saved.</param>
    /// <returns>The saved entity.</returns>
    T Save(T entity);

    /// <summary>
    /// Updates an existing entity in the database.
    /// </summary>
    /// <param name="entity">The updated entity.</param>
    /// <returns>The updated Entity.</returns>
    T Update(T entity);

    /// <summary>
    /// Deletes an entity from the repository.
    /// </summary>
    /// <param name="entity">The entity to be deleted.</param>
    /// <returns>The deleted entity.</returns>
    T Delete(T entity);

    /// <summary>
    /// Retrieves an entity from the repository based on the provided criteria.
    /// </summary>
    /// <param name="criteria">The criteria used to filter entities.</param>
    /// <returns>The entity matching the criteria.</returns>
    T? GetByCriteria(Func<T, bool> criteria);
}
