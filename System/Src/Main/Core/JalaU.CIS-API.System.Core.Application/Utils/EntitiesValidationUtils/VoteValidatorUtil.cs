//-----------------------------------------------------------------------
// <copyright file="VoteValidatorUtil.cs" company="Jala University">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System.Net;
using AutoMapper;
using JalaU.CIS_API.System.Core.Domain;

namespace JalaU.CIS_API.System.Core.Application;

/// <summary>
/// Provides utility methods for validating Vote entities.
/// </summary>
public class VoteValidatorUtil : AbstractValidator<Vote>
{
    private readonly Mapper topicMapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="VoteValidatorUtil"/> class.
    /// </summary>
    public VoteValidatorUtil()
    {
        var mapperConfigurationForTopics = new MapperConfiguration(configuration =>
        {
            configuration.CreateMap<VoteRequestDTO, Vote>().ReverseMap();
        });

        this.topicMapper = new Mapper(mapperConfigurationForTopics);
    }

    /// <inheritdoc/>
    public override Vote ValidateEntityToSave(BaseRequestDTO baseRequestDTO)
    {
        this.MessageLogDTOs = [];
        VoteRequestDTO voteRequestDTO = this.ValidateVoteDTO(baseRequestDTO);

        this.MessageLogDTOs.AddRange(
            EntityValidatorUtil.ValidateBlankOrNullEntityFields(voteRequestDTO)
        );
        Vote? vote = this.topicMapper.Map<Vote>(voteRequestDTO);
        return vote;
    }

    /// <inheritdoc/>
    public override Vote ValidateEntityToUpdate(
        Vote existingVoteToUpdate,
        BaseRequestDTO baseRequestDTO
    )
    {
        this.MessageLogDTOs = new List<MessageLogDTO>();

        UpdateVoteRequestDTO updateRequestDTO = (UpdateVoteRequestDTO)baseRequestDTO;

        existingVoteToUpdate.Positive = updateRequestDTO.Positive;

        EntityValidatorUtil.ValidateBlankOrNullEntityFields(existingVoteToUpdate);

        return existingVoteToUpdate;
    }

    /// <summary>
    /// Validates and casts a BaseRequestDTO to a VoteRequestDTO.
    /// </summary>
    /// <param name="baseRequestDTO">The BaseRequestDTO to validate and cast.</param>
    /// <returns>The validated TopicRequestDTO.</returns>
    /// <exception cref="WrongDataException">Thrown when the validation fails.</exception>
    private VoteRequestDTO ValidateVoteDTO(BaseRequestDTO baseRequestDTO)
    {
        try
        {
            return (VoteRequestDTO)baseRequestDTO;
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
