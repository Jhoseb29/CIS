//-----------------------------------------------------------------------
// <copyright file="TopicService.cs" company="Jala University">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace JalaU.CIS_API.System.Core.Application;

using JalaU.CIS_API.System.Core.Domain;

/// <summary>
/// Represents a service for managing topics.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="TopicService"/> class.
/// </remarks>
/// <param name="topicRepository">The repository for topics.</param>
public class TopicService(IRepository<Topic> topicRepository) : IService<Topic>
{
    private readonly IRepository<Topic> topicRepository = topicRepository;

    /// <inheritdoc/>
    public List<Topic> GetAll()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Topic GetById(Guid guid)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Topic Save(BaseRequestDTO entityToSave)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Topic Update(BaseRequestDTO entityToSave, string id)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Topic DeleteById(Guid guid)
    {
        throw new NotImplementedException();
    }
}
