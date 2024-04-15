//-----------------------------------------------------------------------
// <copyright file="IService.cs" company="Jala University">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace JalaU.CIS_API.System.Core.Domain;

/// <summary>
/// Represents a generic service interface.
/// </summary>
/// <typeparam name="T">The type of entity handled by the service.</typeparam>
public interface IService<T>
{
    /// <summary>
    /// Retrieves all entities from the service.
    /// </summary>
    /// <returns>A list of entities.</returns>
    List<T> GetAll();

    /// <summary>
    /// Retrieves an entity from the service by its unique identifier.
    /// </summary>
    /// <param name="guid">The unique identifier of the entity.</param>
    /// <returns>The entity with the specified identifier.</returns>
    T GetById(Guid guid);

    /// <summary>
    /// Retrieves an entity from the service by its unique identifier.
    /// </summary>
    /// <param name="title">The unique identifier of the entity.</param>
    /// <returns>The entity with the specified identifier.</returns>
    T GetByTitle(string title);

    /// <summary>
    /// Filter the Topics by filters.
    /// </summary>
    /// <param name="filter">The type of filter that will be applied.</param>
    /// <param name="keyword">The key word to apply the filter.</param>
    /// <returns>The entity with the characteristics asked.</returns>
    List<T> FilterEntities(string filter, string keyword);

    /// <summary>
    /// Saves an entity to the service.
    /// </summary>
    /// <param name="entityToSave">The entity to be saved.</param>
    /// <returns>The saved entity.</returns>
    T Save(Topic entityToSave);

    /// <summary>
    /// Updates an entity in the service.
    /// </summary>
    /// <param name="entityToSave">The entity to be updated.</param>
    /// <param name="id">The identifier of the entity to update.</param>
    /// <returns>The updated entity.</returns>
    T Update(BaseRequestDTO entityToSave, string id);

    /// <summary>
    /// Deletes an entity from the service by its unique identifier.
    /// </summary>
    /// <param name="guid">The unique identifier of the entity to delete.</param>
    /// <returns>The deleted entity.</returns>
    T DeleteById(Guid guid);
}
