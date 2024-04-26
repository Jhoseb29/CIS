//-----------------------------------------------------------------------
// <copyright file="VoteService.cs" company="Jala University">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using JalaU.CIS_API.System.Core.Domain;

namespace JalaU.CIS_API.System.Core.Application;

/// <summary>
/// Represents a service for managing ideas.
/// </summary>
/// <param name="ideaRepository">The repository for Vote entities.</param>
/// <param name="ideaService">The service for Idea entities.</param>
/// <param name="validator">The validator for Vote entities.</param>
/// <param name="entityFilter">The entityFilter for Vote entities.</param>
public class VoteService(
    IRepository<Vote> ideaRepository,
    IService<Idea> ideaService,
    AbstractValidator<Vote> validator,
    EntityFilter<Vote> entityFilter
) : IService<Vote>
{
    private readonly IRepository<Vote> voteRepository = ideaRepository;
    private readonly IService<Idea> ideaService = ideaService;
    private readonly EntityFilter<Vote> filters = entityFilter;

    private AbstractValidator<Vote> Validator { get; set; } = validator;

    /// <inheritdoc/>
    public Vote DeleteById(string guid)
    {
        Vote? vote = this.GetByCriteria("id", guid);
        this.voteRepository.Delete(vote);
        return vote;
    }

    /// <inheritdoc/>
    public List<Vote> GetAll(GetAllEntitiesRequestDTO getAllEntitiesRequestDTO)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Vote GetByCriteria(string field, string valueToSearch)
    {
        var vote =
            field.ToLower() switch
            {
                "id" => this.GetById(valueToSearch),
                _ => throw new ArgumentException("Invalid field."),
            }
            ?? throw new EntityNotFoundException(
                $"Idea with the field {field} and the value {valueToSearch} was not found."
            );
        return vote;
    }

    /// <inheritdoc/>
    public Vote Save(BaseRequestDTO entityToSave)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Vote Update(BaseRequestDTO entityToSave, string id)
    {
        var existingVoteToUpdate = this.GetByCriteria("id", id);
        Vote updatedVote = this.Validator.ValidateEntityToUpdate(
            existingVoteToUpdate,
            entityToSave
        );
        this.Validator.AreThereErrors();
        return this.voteRepository.Update(updatedVote);
    }

    private Vote? GetById(string id)
    {
        Guid validGuid = GuidValidatorUtil.ValidateGuid(id);
        return this.voteRepository.GetByCriteria(vote => vote.Id == validGuid);
    }
}
