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
            configuration.CreateMap<UpdateIdeaRequestDTO, Idea>()
                         .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                         .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
        });

        this.topicMapper = new Mapper(mapperConfigurationForTopics);
    }

    /// <inheritdoc/>
    public override Idea ValidateEntityToSave(BaseRequestDTO baseRequestDTO)
    {
        this.MessageLogDTOs = new List<MessageLogDTO>();

        if (baseRequestDTO is IdeaRequestDTO ideaRequestDTO)
        {
            this.MessageLogDTOs.AddRange(
                EntityValidatorUtil.ValidateBlankOrNullEntityFields(ideaRequestDTO)
            );
            return this.topicMapper.Map<Idea>(ideaRequestDTO);
        }
        else if (baseRequestDTO is UpdateIdeaRequestDTO updateIdeaRequestDTO)
        {
            var ideaToUpdate = this.topicMapper.Map<Idea>(updateIdeaRequestDTO);
            this.MessageLogDTOs.AddRange(
                EntityValidatorUtil.ValidateBlankOrNullEntityFields(ideaToUpdate)
            );
            return ideaToUpdate;
        }
        else
        {
            throw new WrongDataException("errors", this.MessageLogDTOs);
        }
    }

    /// <inheritdoc/>
    public override Idea ValidateEntityToUpdate(
        Idea existingIdeaToUpdate,
        BaseRequestDTO baseRequestDTO
    )
    {
        this.MessageLogDTOs = new List<MessageLogDTO>();

        UpdateIdeaRequestDTO updateIdeaRequestDTO = (UpdateIdeaRequestDTO)baseRequestDTO;
        existingIdeaToUpdate.Title = updateIdeaRequestDTO.Title;
        existingIdeaToUpdate.Description = updateIdeaRequestDTO.Description;

        EntityValidatorUtil.ValidateBlankOrNullEntityFields(existingIdeaToUpdate);

        return existingIdeaToUpdate;
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
}
