//-----------------------------------------------------------------------
// <copyright file="TopicService.cs" company="Jala University">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace JalaU.CIS_API.System.Core.Application;

using AutoMapper;
using JalaU.CIS_API.System.Core.Domain;

/// <summary>
/// Represents a service for managing topics.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="TopicService"/> class.
/// </remarks>
public class TopicService : IService<Topic>
{
    private readonly Mapper topicMapper;
    private readonly IRepository<Topic> topicRepository;

    private IValidator<Topic> Validator { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="TopicService"/> class.
    /// </summary>
    /// <param name="topicRepository">The repository for Topic entities.</param>
    /// <param name="validator">The validator for Topic entities.</param>
    public TopicService(IRepository<Topic> topicRepository, IValidator<Topic> validator)
    {
        this.topicRepository = topicRepository;
        this.Validator = validator;
        var mapperConfigurationForTopics = new MapperConfiguration(configuration =>
        {
            configuration.CreateMap<TopicRequestDTO, Topic>().ReverseMap();
        });

        this.topicMapper = new Mapper(mapperConfigurationForTopics);
    }

    /// <inheritdoc/>
    public List<Topic> GetAll()
    {
        List<Topic> topicList = this.topicRepository.GetAll().ToList();
        return topicList;
    }

    /// <inheritdoc/>
    public Topic GetById(Guid guid)
    {
        Topic? topic = this.topicRepository.GetById(guid);
        return topic!;
    }

    /// <inheritdoc/>
    public Topic GetByTitle(string title)
    {
        Topic? topic = this.topicRepository.GetByTitle(title);
        return topic!;
    }

    /// <inheritdoc/>
    public Topic Save(BaseRequestDTO entityToSave)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Topic Update(BaseRequestDTO entityRequestDTO, Guid id)
    {
        var existingTopicToUpdate = this.GetById(id);

        // Move this validation to the GetByCriteria method.
        EntityValidatorUtil.ValidateEntityIsNotNull<Topic>(
            existingTopicToUpdate,
            $"The Topic with the ID {id} couldn't be found"
        );

        // Add Duplicates by Title validation when Joann changes his code.
        Topic updatedTopic = this.Validator.ValidateEntityToUpdate(
            existingTopicToUpdate,
            entityRequestDTO
        );

        return this.topicRepository.Update(updatedTopic);
    }

    /// <inheritdoc/>
    public Topic DeleteById(Guid guid)
    {
        throw new NotImplementedException();
    }
}
