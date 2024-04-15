//-----------------------------------------------------------------------
// <copyright file="TopicValidatorUtil.cs" company="Jala University">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.Net;
using JalaU.CIS_API.System.Core.Domain;

namespace JalaU.CIS_API.System.Core.Application;

/// <summary>
/// Provides utility methods for validating Topic entities.
/// </summary>
public class TopicValidatorUtil : IValidator<Topic>
{
    /// <summary>
    /// Gets or sets list of message logs generated during entity validation.
    /// </summary>
    public List<MessageLogDTO> MessageLogDTOs { get; set; } = [];

    /// <inheritdoc/>
    public void ValidateEntityToSave(BaseRequestDTO baseRequestDTO)
    {
        this.MessageLogDTOs = [];
        TopicRequestDTO topicRequestDTO = this.ValidateTopicDTO(baseRequestDTO);

        this.MessageLogDTOs.AddRange(
            EntityValidatorUtil.ValidateBlankOrNullEntityFields(baseRequestDTO)
        );
        if (this.MessageLogDTOs.Count > 0)
        {
            throw new WrongDataException("errors", this.MessageLogDTOs);
        }
    }

    /// <inheritdoc/>
    public Topic ValidateEntityToUpdate(Topic existingTopicToUpdate, BaseRequestDTO baseRequestDTO)
    {
        this.MessageLogDTOs = [];

        TopicRequestDTO topicRequestDTO = this.ValidateTopicDTO(baseRequestDTO);
        Topic updatedTopic = UpdatableEntityUtil<Topic>.UpdateEntities(
            existingTopicToUpdate,
            topicRequestDTO
        );

        EntityValidatorUtil.ValidateBlankOrNullEntityFields(updatedTopic);

        if (this.MessageLogDTOs.Count > 0)
        {
            throw new WrongDataException("errors", this.MessageLogDTOs);
        }

        return updatedTopic;
    }

    /// <summary>
    /// Validates and casts a BaseRequestDTO to a TopicRequestDTO.
    /// </summary>
    /// <param name="baseRequestDTO">The BaseRequestDTO to validate and cast.</param>
    /// <returns>The validated TopicRequestDTO.</returns>
    /// <exception cref="WrongDataException">Thrown when the validation fails.</exception>
    private TopicRequestDTO ValidateTopicDTO(BaseRequestDTO baseRequestDTO)
    {
        try
        {
            return (TopicRequestDTO)baseRequestDTO;
        }
        catch (Exception exception)
        {
            this.MessageLogDTOs.Add(
                new MessageLogDTO((int)HttpStatusCode.UnprocessableEntity, exception.Message)
            );
            throw new WrongDataException("erros", this.MessageLogDTOs);
        }
    }
}
