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
    private readonly EntityFilter<Idea> filters;

    private IValidator<Idea> Validator { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="IdeaService"/> class.
    /// </summary>
    /// <param name="ideaRepository">The repository for Idea entities.</param>
    /// <param name="validator">The validator for Idea entities.</param>
    /// <param name="entityFilter">The entityFilter for Idea entities.</param>
    public IdeaService(
    IRepository<Idea> ideaRepository,
    IValidator<Idea> validator,
    EntityFilter<Idea> entityFilter)
    {
        this.ideaRepository = ideaRepository;
        this.Validator = validator;
        this.filters = entityFilter;
        var mapperConfigurationForTopics = new MapperConfiguration(configuration =>
        {
            configuration.CreateMap<IdeaRequestDTO, Idea>().ReverseMap();
        });

        this.ideaMapper = new Mapper(mapperConfigurationForTopics);
    }

    /// <inheritdoc/>
    public List<Idea> GetAll()
    {
        throw new NotImplementedException();
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
                "id" => this.GetById(Guid.Parse(valueToSearch)),
                _ => throw new ArgumentException("Invalid field."),
            }

            ?? throw new EntityNotFoundException(
                $"Idea with the field {field} and the value {valueToSearch} was not found.");
        return idea;
    }

    /// <inheritdoc/>
    public List<Idea> FilterEntities(string filter, string keyword)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Idea Save(BaseRequestDTO entityToSave)
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

    private Idea? GetByTitle(string title)
    {
        return this.ideaRepository.GetByCriteria(idea => idea.Title == title);
    }

    private Idea? GetById(Guid id)
    {
        Guid validGuid = GuidValidatorUtil.ValidateGuid(id);
        return this.ideaRepository.GetByCriteria(idea => idea.Id == id);
    }
}
