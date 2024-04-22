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
    /// <param name="getAllEntitiesRequestDTO">The query strings.</param>
    /// <returns>A list of entities.</returns>
    List<T> GetAll(GetAllEntitiesRequestDTO getAllEntitiesRequestDTO);

    /// <summary>
    /// Filter the entities by criteria.
    /// </summary>
    /// <param name="field">The field to search for.</param>
    /// <param name="valueToSearch">The value to search for.</param>
    /// <returns>The entity with the specified criteria.</returns>
    public T GetByCriteria(string field, string valueToSearch);

    /// <summary>
    /// Saves an entity to the service.
    /// </summary>
    /// <param name="entityToSave">The entity to be saved.</param>
    /// <returns>The saved entity.</returns>
    T Save(BaseRequestDTO entityToSave);

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
    T DeleteById(string guid);
}
