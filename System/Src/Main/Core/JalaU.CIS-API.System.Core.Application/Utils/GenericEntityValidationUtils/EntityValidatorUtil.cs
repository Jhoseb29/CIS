//-----------------------------------------------------------------------
// <copyright file="EntityValidatorUtil.cs" company="Jala University">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.Net;
using JalaU.CIS_API.System.Core.Domain;

namespace JalaU.CIS_API.System.Core.Application;

/// <summary>
/// Abstract class providing utility methods for validating entities.
/// </summary>
public sealed class EntityValidatorUtil
{
    /// <summary>
    /// Validates that an entity is not null.
    /// </summary>
    /// <typeparam name="T">The type of entity.</typeparam>
    /// <param name="objectToValidate">The entity to validate.</param>
    /// <param name="possibleErrorMessage">The error message to throw if the entity is null.</param>
    public static void ValidateEntityIsNotNull<T>(T? objectToValidate, string possibleErrorMessage)
    {
        if (objectToValidate == null)
        {
            throw new EntityNotFoundException(possibleErrorMessage);
        }
    }

    /// <summary>
    /// Validates that no fields of an entity are blank or null.
    /// </summary>
    /// <typeparam name="T">The type of entity.</typeparam>
    /// <param name="objectToValidate">The entity to validate.</param>
    /// <returns>A list of message logs indicating any blank or null fields.</returns>
    public static List<MessageLogDTO> ValidateBlankOrNullEntityFields<T>(T objectToValidate)
    {
        List<MessageLogDTO> messageLogs = [];

        var properties = objectToValidate!.GetType().GetProperties();

        foreach (var property in properties)
        {
            var value = property.GetValue(objectToValidate);

            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                messageLogs.Add(
                    new MessageLogDTO(
                        (int)HttpStatusCode.UnprocessableEntity,
                        $"The field {property.Name} can't be null or empty"
                    )
                );
            }
        }

        return messageLogs;
    }
}
