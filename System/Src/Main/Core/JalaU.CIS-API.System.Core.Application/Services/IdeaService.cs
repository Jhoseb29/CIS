﻿//-----------------------------------------------------------------------
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

    private AbstractValidator<Idea> Validator { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="IdeaService"/> class.
    /// </summary>
    /// <param name="ideaRepository">The repository for Idea entities.</param>
    /// <param name="validator">The validator for Idea entities.</param>
    /// <param name="entityFilter">The entityFilter for Idea entities.</param>
    public IdeaService(
        IRepository<Idea> ideaRepository,
        AbstractValidator<Idea> validator,
        EntityFilter<Idea> entityFilter
    )
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
        throw new NotImplementedException();
    }
}