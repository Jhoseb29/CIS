//-----------------------------------------------------------------------
// <copyright file="TopicValidatorUtil.cs" company="Jala University">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.Net;
using System.Reflection;
using AutoMapper;
using JalaU.CIS_API.System.Core.Domain;

namespace JalaU.CIS_API.System.Core.Application;

/// <summary>
/// Provides utility methods for validating Topic entities.
/// </summary>
public class TopicValidatorUtil : AbstractValidator<Topic>
{
    private readonly Mapper topicMapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="TopicValidatorUtil"/> class.
    /// </summary>
    public TopicValidatorUtil()
    {
        var mapperConfigurationForTopics = new MapperConfiguration(configuration =>
        {
            configuration.CreateMap<TopicRequestDTO, Topic>().ReverseMap();
        });

        this.topicMapper = new Mapper(mapperConfigurationForTopics);
    }

    /// <inheritdoc/>
    public override Topic ValidateEntityToSave(BaseRequestDTO baseRequestDTO)
    {
        this.MessageLogDTOs = [];
        TopicRequestDTO topicRequestDTO = this.ValidateTopicDTO(baseRequestDTO);

        this.MessageLogDTOs.AddRange(
            EntityValidatorUtil.ValidateBlankOrNullEntityFields(topicRequestDTO)
        );
        return this.topicMapper.Map<Topic>(topicRequestDTO);
    }

    /// <inheritdoc/>
    public override Topic ValidateEntityToUpdate(
        Topic existingTopicToUpdate,
        BaseRequestDTO baseRequestDTO
    )
    {
        this.MessageLogDTOs = [];

        TopicRequestDTO topicRequestDTO = this.ValidateTopicDTO(baseRequestDTO);
        Topic updatedTopic = UpdatableEntityUtil<Topic>.UpdateEntities(
            existingTopicToUpdate,
            topicRequestDTO
        );

        EntityValidatorUtil.ValidateBlankOrNullEntityFields(updatedTopic);

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
            throw new WrongDataException("errors", this.MessageLogDTOs);
        }
    }
}
