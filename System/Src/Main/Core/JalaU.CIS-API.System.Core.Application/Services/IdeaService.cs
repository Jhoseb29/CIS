//-----------------------------------------------------------------------
// <copyright file="IdeaService.cs" company="Jala University">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace JalaU.CIS_API.System.Core.Application;

using AutoMapper;
using JalaU.CIS_API.System.Core.Domain;

/// <summary>
/// Represents a service for managing ideas.
/// </summary>
/// <param name="ideaRepository">The repository for Idea entities.</param>
/// <param name="topicService">The service for Topic entities.</param>
/// <param name="validator">The validator for Idea entities.</param>
/// <param name="entityFilter">The entityFilter for Idea entities.</param>
public class IdeaService(
    IRepository<Idea> ideaRepository,
    IService<Topic> topicService,
    AbstractValidator<Idea> validator,
    EntityFilter<Idea> entityFilter
) : IService<Idea>
{
    private readonly IRepository<Idea> ideaRepository = ideaRepository;
    private readonly IService<Topic> topicService = topicService;
    private readonly EntityFilter<Idea> filters = entityFilter;

    private AbstractValidator<Idea> Validator { get; set; } = validator;

    /// <inheritdoc/>
    public Idea Save(BaseRequestDTO ideaToSave)
    {
        Idea ideaValidated = this.Validator.ValidateEntityToSave(ideaToSave);
        this.Validator.CheckDuplicateEntity(
            this.GetByTitleWithinATopic(ideaValidated.Title, ideaValidated.TopicId.ToString())!,
            "The Idea's title is already registered within the Topic associated."
        );
        this.Validator.AreThereErrors();

        Topic topic =
            this.topicService.GetByCriteria("id", ideaValidated.TopicId.ToString())
            ?? throw new EntityNotFoundException(
                "The associated Topic doesn't exist in the System."
            );

        ideaValidated.UserId = GuidValidatorUtil.ValidateGuid(GlobalVariables.UserId!);

        var idea = this.ideaRepository.Save(ideaValidated);
        return idea;
    }

    /// <inheritdoc/>
    public List<Idea> GetAll(GetAllEntitiesRequestDTO getAllEntitiesRequestDTO)
    {
        List<Idea> ideaList = this.ideaRepository.GetAll().ToList();
        List<string> fieldsAllowedToOrderBy = ["title", "date"];

        EntitiesListParameterizerUtil<Idea> entitiesListParameterizerUtil =
            new(ideaList, fieldsAllowedToOrderBy);
        var finalIdeasListToReturn = entitiesListParameterizerUtil.ApplyGetAllParameters(
            this.filters,
            getAllEntitiesRequestDTO
        );

        if (finalIdeasListToReturn.Count == 0)
        {
            throw new EntityNotFoundException("Any Idea were found.");
        }
        return finalIdeasListToReturn;
    }

    /// <summary>
    /// Retrieves an idea based on the specified field and value using HTTP GET method.
    /// </summary>
    /// <param name="field">The field to search by (e.g., "id" or "title").</param>
    /// <param name="valueToSearch">The value to search for in the specified field.</param>
    /// <returns>
    /// An instance of the Idea class if an idea with the specified field and value is found.
    /// Throws EntityNotFoundException if no idea is found with the specified field and value.
    /// Throws ArgumentException if the specified field is invalid.
    /// </returns>
    public Idea GetByCriteria(string field, string valueToSearch)
    {
        var idea =
            field.ToLower() switch
            {
                "id" => this.GetById(valueToSearch),
                "title" => this.GetByTitle(valueToSearch),
                _ => throw new ArgumentException("Invalid field."),
            }
            ?? throw new EntityNotFoundException(
                $"Idea with the field {field} and the value {valueToSearch} was not found."
            );
        return idea;
    }

    /// <inheritdoc/>
    public Idea Update(BaseRequestDTO ideaToUpdate, string id)
    {
        var existingIdeaToUpdate = this.GetByCriteria("id", id);
        Idea updatedIdea = this.Validator.ValidateEntityToUpdate(
            existingIdeaToUpdate!,
            ideaToUpdate
        );

        return this.ideaRepository.Update(updatedIdea);
    }

    /// <inheritdoc/>
    public Idea DeleteById(string guid)
    {
        Idea? idea = this.GetByCriteria("id", guid);
        this.ideaRepository.Delete(idea);
        return idea;
    }

    private Idea? GetByTitle(string title)
    {
        return this.ideaRepository.GetByCriteria(idea => idea.Title == title);
    }

    private Idea? GetById(string id)
    {
        Guid validGuid = GuidValidatorUtil.ValidateGuid(id);
        return this.ideaRepository.GetByCriteria(idea => idea.Id == validGuid);
    }

    private Idea? GetByTitleWithinATopic(string? title, string idTopic)
    {
        Guid validGuid = GuidValidatorUtil.ValidateGuid(idTopic);
        return this.ideaRepository.GetByCriteria(
            idea => idea.Title == title && idea.TopicId == validGuid
        );
    }
}
