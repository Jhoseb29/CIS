//-----------------------------------------------------------------------
// <copyright file="IdeaValidatorUtil.cs" company="Jala University">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.Net;
using JalaU.CIS_API.System.Core.Domain;

namespace JalaU.CIS_API.System.Core.Application;

/// <summary>
/// Provides utility methods for validating Topic entities.
/// </summary>
public class IdeaValidatorUtil : IValidator<Idea>
{
    /// <summary>
    /// Gets or sets list of message logs generated during entity validation.
    /// </summary>
    public List<MessageLogDTO> MessageLogDTOs { get; set; } = [];

    /// <inheritdoc/>
    public void ValidateEntityToSave(BaseRequestDTO baseRequestDTO) { }

    /// <inheritdoc/>
    public Idea ValidateEntityToUpdate(Idea existingTopicToUpdate, BaseRequestDTO baseRequestDTO)
    {
        return null!;
    }

    /// <summary>
    /// Validates and casts a BaseRequestDTO to a TopicRequestDTO.
    /// </summary>
    /// <param name="baseRequestDTO">The BaseRequestDTO to validate and cast.</param>
    /// <returns>The validated TopicRequestDTO.</returns>
    /// <exception cref="WrongDataException">Thrown when the validation fails.</exception>
    private TopicRequestDTO ValidateIdeaDTO(BaseRequestDTO baseRequestDTO)
    {
        return null!;
    }
}
