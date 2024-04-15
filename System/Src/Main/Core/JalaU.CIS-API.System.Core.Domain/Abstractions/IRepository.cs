//-----------------------------------------------------------------------
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
    /// Retrieves an entity from the repository by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the entity.</param>
    /// <returns>The entity with the specified identifier.</returns>
    T? GetById(Guid id);

    /// <summary>
    /// Retrieves an entity from the repository by its unique identifier.
    /// </summary>
    /// <param name="title">The unique identifier of the entity.</param>
    /// <returns>The entity with the specified identifier.</returns>
    T? GetByTitle(string title);

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
}
