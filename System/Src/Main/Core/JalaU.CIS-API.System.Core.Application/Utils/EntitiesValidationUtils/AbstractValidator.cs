//-----------------------------------------------------------------------
// <copyright file="AbstractValidator.cs" company="Jala University">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.Net;
using JalaU.CIS_API.System.Core.Domain;

namespace JalaU.CIS_API.System.Core.Application;

/// <summary>
/// Interface for validating entities of type T.
/// </summary>
/// <typeparam name="T">The type of entity to validate.</typeparam>
public abstract class AbstractValidator<T>
{
    /// <summary>
    /// Gets or sets list of message logs generated during entity validation.
    /// </summary>
    public List<MessageLogDTO> MessageLogDTOs { get; set; } = [];

    /// <summary>
    /// Validates an entity before saving.
    /// </summary>
    /// <param name="baseRequestDTO">The DTO representing the entity to validate.</param>
    /// <returns>The validated entity of type T.</returns>
    public abstract T ValidateEntityToSave(BaseRequestDTO baseRequestDTO);

    /// <summary>
    /// Validates an entity before updating.
    /// </summary>
    /// <param name="value">The entity to validate.</param>
    /// <param name="baseRequestDTO">The DTO representing the entity to validate.</param>
    /// <returns>The validated entity of type T.</returns>
    public abstract T ValidateEntityToUpdate(T value, BaseRequestDTO baseRequestDTO);

    /// <summary>
    /// Checks if the provided entity already exists in the system and throws an exception with the specified error message if duplicated.
    /// </summary>
    /// <param name="entity">The entity to check for duplication.</param>
    /// <param name="errorMessageIfDuplicated">The error message to throw if the entity is duplicated.</param>
    public virtual void CheckDuplicateEntity(T entity, string errorMessageIfDuplicated)
    {
        if (entity != null)
        {
            this.MessageLogDTOs.Add(
                new MessageLogDTO((int)HttpStatusCode.Conflict, errorMessageIfDuplicated)
            );
        }
    }

    /// <summary>
    /// Checks if there are any errors logged in the message log DTOs and throws a <see cref="WrongDataException"/> if errors are found.
    /// </summary>
    /// <exception cref="WrongDataException">Thrown when errors are found in the message log DTOs.</exception>
    public void AreThereErrors()
    {
        if (this.MessageLogDTOs.Count > 0)
        {
            throw new WrongDataException("errors", this.MessageLogDTOs);
        }
    }

    /// <summary>
    /// Checks for the presence of bad words in a specified field of the provided instance.
    /// If any bad words are found, logs a message indicating the presence of the bad word.
    /// </summary>
    /// <param name="fieldName">The name of the field for which we check for bad words.</param>
    /// <param name="fieldToAnalyze">The field in which to check for bad words.</param>
    protected void CheckBadWords(string fieldName, string fieldToAnalyze)
    {
        if (BadWordsChecker.BadWordsInText(fieldToAnalyze))
        {
            this.MessageLogDTOs.Add(
                new MessageLogDTO(
                    (int)HttpStatusCode.UnprocessableContent,
                    $"There's a bad word and can't be used in the Entity's {fieldName}."
                )
            );
        }
    }
}
