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
    public void ValidateEntityToSave(BaseRequestDTO baseRequestDTO)
    {
        this.MessageLogDTOs = [];
        IdeaRequestDTO ideaRequestDTO = this.ValidateIdeaDTO(baseRequestDTO);

        this.MessageLogDTOs.AddRange(
            EntityValidatorUtil.ValidateBlankOrNullEntityFields(baseRequestDTO)
        );
        if (this.MessageLogDTOs.Count > 0)
        {
            throw new WrongDataException("errors", this.MessageLogDTOs);
        }
     }

    /// <inheritdoc/>
    public Idea ValidateEntityToUpdate(Idea existingIdeaToUpdate, BaseRequestDTO baseRequestDTO)
    {
        this.MessageLogDTOs = [];

        IdeaRequestDTO ideaRequestDTO = this.ValidateIdeaDTO(baseRequestDTO);
        Idea updatedIdea = UpdatableEntityUtil<Idea>.UpdateEntities(
            existingIdeaToUpdate,
            ideaRequestDTO
        );

        EntityValidatorUtil.ValidateBlankOrNullEntityFields(updatedIdea);

        if (this.MessageLogDTOs.Count > 0)
        {
            throw new WrongDataException("errors", this.MessageLogDTOs);
        }

        return updatedIdea;
    }

    /// <summary>
    /// Validates and casts a BaseRequestDTO to a IdeaRequestDTO.
    /// </summary>
    /// <param name="baseRequestDTO">The BaseRequestDTO to validate and cast.</param>
    /// <returns>The validated IdeaRequestDTO.</returns>
    /// <exception cref="WrongDataException">Thrown when the validation fails.</exception>
    private IdeaRequestDTO ValidateIdeaDTO(BaseRequestDTO baseRequestDTO)
    {
        try
        {
            return (IdeaRequestDTO)baseRequestDTO;
        }
        catch (Exception exception)
        {
            this.MessageLogDTOs.Add(
                new MessageLogDTO((int)HttpStatusCode.UnprocessableEntity, exception.Message)
            );
            throw new WrongDataException("errors", this.MessageLogDTOs);
        }
    }
}
