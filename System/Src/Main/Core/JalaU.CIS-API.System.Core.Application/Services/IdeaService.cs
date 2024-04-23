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
/// <remarks>
/// Initializes a new instance of the <see cref="IdeaService"/> class.
/// </remarks>
public class IdeaService : IService<Idea>
{
    private readonly Mapper ideaMapper;
    private readonly IRepository<Idea> ideaRepository;
    private readonly IService<Topic> topicService;
    private readonly EntityFilter<Idea> filters;

    private AbstractValidator<Idea> Validator { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="IdeaService"/> class.
    /// </summary>
    /// <param name="ideaRepository">The repository for Idea entities.</param>
    /// <param name="topicService">The service for Topic entities.</param>
    /// <param name="validator">The validator for Idea entities.</param>
    /// <param name="entityFilter">The entityFilter for Idea entities.</param>
    public IdeaService(
        IRepository<Idea> ideaRepository,
        IService<Topic> topicService,
        AbstractValidator<Idea> validator,
        EntityFilter<Idea> entityFilter
    )
    {
        this.ideaRepository = ideaRepository;
        this.topicService = topicService;
        this.Validator = validator;
        this.filters = entityFilter;
        var mapperConfigurationForTopics = new MapperConfiguration(configuration =>
        {
            configuration.CreateMap<IdeaRequestDTO, Idea>().ReverseMap();
        });

        this.ideaMapper = new Mapper(mapperConfigurationForTopics);
    }

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

        // Más adelante obtener el ID del usuario mediante el JWT y ponerlo aquí:
        ideaValidated.UserId = GuidValidatorUtil.ValidateGuid(
            "550e8400-e29b-41d4-a716-446655440000"
        );

        var idea = this.ideaRepository.Save(ideaValidated);
        return idea;
    }

    /// <inheritdoc/>
    public List<Idea> GetAll(GetAllEntitiesRequestDTO getAllEntitiesRequestDTO)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Idea GetByCriteria(string field, string valueToSearch)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Idea Update(BaseRequestDTO entityToSave, string id)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Idea DeleteById(string guid)
    {
        Idea? idea = this.GetByCriteria("id", guid);
        this.ideaRepository.Delete(idea);
        return idea;
    }

    private Idea? GetByTitleWithinATopic(string title, string idTopic)
    {
        Guid validGuid = GuidValidatorUtil.ValidateGuid(idTopic);
        return this.ideaRepository.GetByCriteria(
            idea => idea.Title == title && idea.TopicId == validGuid
        );
    }
}
