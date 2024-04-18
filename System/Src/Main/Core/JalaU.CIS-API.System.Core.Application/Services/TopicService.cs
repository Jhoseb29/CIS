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
    private readonly EntityFilter<Topic> filters;

    private IValidator<Topic> Validator { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="TopicService"/> class.
    /// </summary>
    /// <param name="topicRepository">The repository for Topic entities.</param>
    /// <param name="validator">The validator for Topic entities.</param>
    /// <param name="entityFilter">The entityFilter for Topic entities.</param>
    public TopicService(
        IRepository<Topic> topicRepository,
        IValidator<Topic> validator,
        EntityFilter<Topic> entityFilter
    )
    {
        this.topicRepository = topicRepository;
        this.Validator = validator;
        this.filters = entityFilter;
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
    public List<Topic> FilterEntities(string filter, string keyword)
    {
        return this.filters.Filter(this.GetAll(), filter, keyword);
    }

    /// <inheritdoc/>
    public Topic GetByCriteria(string field, string valueToSearch)
    {
        var topic =
            field.ToLower() switch
            {
                "id" => this.GetById(Guid.Parse(valueToSearch)),
                "title" => this.GetByTitle(valueToSearch),
                _ => throw new ArgumentException("Invalid field."),
            }
            ?? throw new EntityNotFoundException(
                $"Topic with the field {field} and the value {valueToSearch} was not found."
            );
        return topic;
    }

    /// <inheritdoc/>
    public Topic Save(BaseRequestDTO entityToSave)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Topic Update(BaseRequestDTO entityRequestDTO, string id)
    {
        var existingTopicToUpdate = this.GetByCriteria("id", id);
        Topic updatedTopic = this.Validator.ValidateEntityToUpdate(
            existingTopicToUpdate!,
            entityRequestDTO
        );

        return this.topicRepository.Update(updatedTopic);
    }

    /// <inheritdoc/>
    public Topic DeleteById(Guid guid)
    {
        Topic? topic = GetById(guid);
        this.topicRepository.Delete(topic);
        return topic;
    }

    private Topic? GetByTitle(string title)
    {
        return this.topicRepository.GetByCriteria(t => t.Title == title);
    }

    private Topic? GetById(Guid id)
    {
        return this.topicRepository.GetByCriteria(t => t.Id == id);
    }
}
