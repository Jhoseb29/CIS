//-----------------------------------------------------------------------
// <copyright file="UpdatableEntityUtil.cs" company="Jala University">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using JalaU.CIS_API.System.Core.Domain;

namespace JalaU.CIS_API.System.Core.Application;

/// <summary>
/// Utility class for updating entities of type T based on the properties of a new version represented by a BaseRequestDTO.
/// </summary>
/// <typeparam name="T">The type of entity to update.</typeparam>
public class UpdatableEntityUtil<T>
{
    /// <summary>
    /// Updates the properties of an entity based on the properties of a new version represented by a BaseRequestDTO.
    /// </summary>
    /// <param name="entityToUpdate">The entity to update.</param>
    /// <param name="newVersionOfEntity">The DTO representing the new version of the entity.</param>
    /// <returns>The updated entity of type T.</returns>
    /// <exception cref="ApplicationException">Thrown when either the entity to update or the new version of the entity is null.</exception>
    public static T UpdateEntities(T entityToUpdate, BaseRequestDTO newVersionOfEntity)
    {
        if (entityToUpdate != null && newVersionOfEntity != null)
        {
            var properties = newVersionOfEntity.GetType().GetProperties();

            foreach (var property in properties)
            {
                var newValue = property.GetValue(newVersionOfEntity);
                if (newValue != null)
                {
                    var entityToUpdateProperty = entityToUpdate
                        .GetType()
                        .GetProperty(property.Name);
                    if (
                        entityToUpdateProperty != null
                        && entityToUpdateProperty.PropertyType == property.PropertyType
                        && entityToUpdateProperty.CanWrite
                    )
                    {
                        entityToUpdateProperty.SetValue(entityToUpdate, newValue);
                    }
                }
            }

            return entityToUpdate;
        }

        throw new ApplicationException("One of the Updatable objects is null.");
    }
}
