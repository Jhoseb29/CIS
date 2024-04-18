//-----------------------------------------------------------------------
// <copyright file="IValidator.cs" company="Jala University">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace JalaU.CIS_API.System.Core.Domain;

/// <summary>
/// Interface for validating entities of type T.
/// </summary>
/// <typeparam name="T">The type of entity to validate.</typeparam>
public interface IValidator<T>
{
    /// <summary>
    /// Validates an entity before saving.
    /// </summary>
    /// <param name="baseRequestDTO">The DTO representing the entity to validate.</param>
    void ValidateEntityToSave(BaseRequestDTO baseRequestDTO);

    /// <summary>
    /// Validates an entity before updating.
    /// </summary>
    /// <param name="value">The entity to validate.</param>
    /// <param name="baseRequestDTO">The DTO representing the entity to validate.</param>
    /// <returns>The validated entity of type T.</returns>
    T ValidateEntityToUpdate(T value, BaseRequestDTO baseRequestDTO);
}
