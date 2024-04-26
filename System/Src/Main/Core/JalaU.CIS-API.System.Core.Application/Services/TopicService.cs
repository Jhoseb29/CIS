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
/// <param name="topicRepository">The repository for Topic entities.</param>
/// <param name="validator">The validator for Topic entities.</param>
/// <param name="entityFilter">The entityFilter for Topic entities.</param>
public class TopicService(
    IRepository<Topic> topicRepository,
    AbstractValidator<Topic> validator,
    EntityFilter<Topic> entityFilter
) : IService<Topic>
{
    private readonly IRepository<Topic> topicRepository = topicRepository;
    private readonly EntityFilter<Topic> topicFilter = entityFilter;

    private AbstractValidator<Topic> Validator { get; set; } = validator;

    /// <inheritdoc/>
    public List<Topic> GetAll(GetAllEntitiesRequestDTO getAllEntitiesRequestDTO)
    {
        List<Topic> topicList = this.topicRepository.GetAll().ToList();
        List<string> fieldsAllowedToOrderBy = ["title", "date"];

        EntitiesListParameterizerUtil<Topic> entitiesListParameterizerUtil =
            new(topicList, fieldsAllowedToOrderBy);
        var finalTopicsListToReturn = entitiesListParameterizerUtil.ApplyGetAllParameters(
            this.topicFilter,
            getAllEntitiesRequestDTO
        );

        if (finalTopicsListToReturn.Count == 0)
        {
            throw new EntityNotFoundException("Any Topics were found.");
        }
        return finalTopicsListToReturn;
    }

    /// <inheritdoc/>
    public Topic GetByCriteria(string field, string valueToSearch)
    {
        var topic =
            field.ToLower() switch
            {
                "id" => this.GetById(valueToSearch),
                "title" => this.GetByTitle(valueToSearch),
                _ => throw new ArgumentException("Invalid field."),
            }
            ?? throw new EntityNotFoundException(
                $"Topic with the field {field} and the value {valueToSearch} was not found."
            );
        return topic;
    }

    /// <inheritdoc/>
    public Topic Save(BaseRequestDTO topicToSave)
    {
        Topic topicValidated = this.Validator.ValidateEntityToSave(topicToSave);
        this.Validator.CheckDuplicateEntity(
            this.GetByTitle(topicValidated.Title)!,
            "The Topic's title is already registered in the System."
        );
        this.Validator.AreThereErrors();

        topicValidated.UserId = GuidValidatorUtil.ValidateGuid(GlobalVariables.UserId!);

        Topic topic = this.topicRepository.Save(topicValidated);
        return topic;
    }

    /// <inheritdoc/>
    public Topic Update(BaseRequestDTO entityRequestDTO, string id)
    {
        var existingTopicToUpdate = this.GetByCriteria("id", id);
        Topic updatedTopic = this.Validator.ValidateEntityToUpdate(
            existingTopicToUpdate!,
            entityRequestDTO
        );
        this.Validator.CheckDuplicateEntity(
            this.GetByTitle(updatedTopic.Title)!,
            "The Topic's title is already registered in the System."
        );
        this.Validator.AreThereErrors();

        return this.topicRepository.Update(updatedTopic);
    }

    /// <inheritdoc/>
    public Topic DeleteById(string guid)
    {
        Topic? topic = this.GetByCriteria("id", guid);
        this.topicRepository.Delete(topic);
        return topic;
    }

    private Topic? GetByTitle(string title)
    {
        return this.topicRepository.GetByCriteria(t => t.Title == title);
    }

    private Topic? GetById(string id)
    {
        Guid validGuid = GuidValidatorUtil.ValidateGuid(id);
        return this.topicRepository.GetByCriteria(t => t.Id == validGuid);
    }
}
