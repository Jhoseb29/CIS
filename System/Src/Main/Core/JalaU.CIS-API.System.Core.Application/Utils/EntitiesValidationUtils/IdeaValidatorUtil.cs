//-----------------------------------------------------------------------
// <copyright file="IdeaValidatorUtil.cs" company="Jala University">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.Net;
using AutoMapper;
using JalaU.CIS_API.System.Core.Domain;

namespace JalaU.CIS_API.System.Core.Application;

/// <summary>
/// Provides utility methods for validating Topic entities.
/// </summary>
public class IdeaValidatorUtil : AbstractValidator<Idea>
{
    private readonly Mapper topicMapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="IdeaValidatorUtil"/> class.
    /// </summary>
    public IdeaValidatorUtil()
    {
        var mapperConfigurationForTopics = new MapperConfiguration(configuration =>
        {
            configuration.CreateMap<IdeaRequestDTO, Idea>().ReverseMap();
            configuration.CreateMap<UpdateIdeaRequestDTO, Idea>().ReverseMap();
        });

        this.topicMapper = new Mapper(mapperConfigurationForTopics);
    }

    /// <inheritdoc/>
    public override Idea ValidateEntityToSave(BaseRequestDTO baseRequestDTO)
    {
        this.MessageLogDTOs = [];
        IdeaRequestDTO ideaRequestDTO = this.ValidateIdeaRequestDTO(baseRequestDTO);

        this.MessageLogDTOs.AddRange(
            EntityValidatorUtil.ValidateBlankOrNullEntityFields(ideaRequestDTO)
        );
        return this.topicMapper.Map<Idea>(ideaRequestDTO);
    }

    /// <inheritdoc/>
    public override Idea ValidateEntityToUpdate(
        Idea existingIdeaToUpdate,
        BaseRequestDTO baseRequestDTO
    )
    {
        this.MessageLogDTOs = new List<MessageLogDTO>();

        UpdateIdeaRequestDTO updateIdeaRequestDTO = this.ValidateUpdateIdeaRequestDTO(baseRequestDTO);
        Idea updatedIdea = UpdatableEntityUtil<Idea>.UpdateEntities(
            existingIdeaToUpdate,
            updateIdeaRequestDTO
        );

        EntityValidatorUtil.ValidateBlankOrNullEntityFields(updatedIdea);

        return updatedIdea;
    }

    /// <summary>
    /// Validates and casts a BaseRequestDTO to an IdeaRequestDTO.
    /// </summary>
    /// <param name="baseRequestDTO">The BaseRequestDTO to validate and cast.</param>
    /// <returns>The validated IdeaRequestDTO.</returns>
    /// <exception cref="WrongDataException">Thrown when the validation fails.</exception>
    private IdeaRequestDTO ValidateIdeaRequestDTO(BaseRequestDTO baseRequestDTO)
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

    /// <summary>
    /// Validates and casts a BaseRequestDTO to an UpdateIdeaRequestDTO.
    /// </summary>
    /// <param name="baseRequestDTO">The BaseRequestDTO to validate and cast.</param>
    /// <returns>The validated UpdateIdeaRequestDTO.</returns>
    /// <exception cref="WrongDataException">Thrown when the validation fails.</exception>
    private UpdateIdeaRequestDTO ValidateUpdateIdeaRequestDTO(BaseRequestDTO baseRequestDTO)
    {
        try
        {
            return (UpdateIdeaRequestDTO)baseRequestDTO;
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
