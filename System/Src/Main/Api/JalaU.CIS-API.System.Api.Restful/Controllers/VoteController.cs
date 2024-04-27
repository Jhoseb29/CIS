//-----------------------------------------------------------------------
// <copyright file="VoteController.cs" company="Jala University">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using global::System.Net;
using JalaU.CIS_API.System.Core.Application;
using JalaU.CIS_API.System.Core.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace JalaU.CIS_API.System.Api.Restful;

/// <summary>
/// Controller for managing votes.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="VoteController"/> class.
/// </remarks>
/// <param name="service">The service instance for managing votes.</param>
[Authorize]
[ApiController]
[Route("cis-api/v1/votes")]
public class VoteController(IService<Vote> service) : ControllerBase
{
    private readonly IService<Vote> service = service;

    /// <summary>
    /// Deletes a topic by its ID using HTTP DELETE method.
    /// </summary>
    /// <param name="voteId">The ID of the topic to be deleted.</param>
    /// <returns>
    /// An HTTP 200 OK response with the updated topic in the body.
    /// An HTTP 400 Bad Request response with all error details.
    /// </returns>
    [HttpDelete("{voteId}")]
    public ActionResult DeleteTopic(string voteId)
    {
        var topic = this.service.DeleteById(voteId);

        return this.Ok(topic);
    }

    /// <summary>
    /// Updates a vote by its ID using HTTP PUT method.
    /// </summary>
    /// <param name="voteRequestDto">The DTO (Data Transfer Object) containing updated vote information.</param>
    /// <param name="voteId">The ID of the vote to be updated.</param>
    /// <returns>
    /// An HTTP 200 OK response with the updated vote in the body.
    /// An HTTP 400 Bad Request response with all error details.
    /// </returns>
    [HttpPut("{voteId}")]
    public ActionResult UpdateVote([FromBody] UpdateVoteRequestDTO voteRequestDto, string voteId)
    {
        var updatedVote = this.service.Update(voteRequestDto, voteId);

        return this.Ok(updatedVote);
    }

    /// <summary>
    /// Gets a vote by its ID using HTTP GET method.
    /// </summary>
    /// <param name="voteId">The ID of the vote to retrieve.</param>
    /// <returns>
    /// An HTTP 200 OK response with the retrieved vote in the body.
    /// An HTTP 400 Bad Request response with all error detail.
    /// An HTTP 422 Unprocessable Entity response if the ID is invalid.
    /// </returns>
    [HttpGet("{voteId}")]
    public ActionResult GetVoteById(string voteId)
    {
        var vote = this.service.GetByCriteria("id", voteId);

        return this.Ok(vote);
    }

    /// <summary>
    /// Gets all votes in the system with pagination support.
    /// </summary>
    /// <param name="pageSize">Optional. The number of topics to include in a page.</param>
    /// <param name="pageNumber">Optional. The page number to retrieve.</param>
    /// <param name="orderBy"> orderBy. </param>
    /// <param name="order"> order.</param>
    /// <param name="filter"> filter.</param>
    /// <param name="keyword">keyword.</param>
    /// <returns>
    /// An HTTP 200 OK response with the retrieved votes in the body.
    /// An HTTP 404 Not Found response if no votes are found.
    /// </returns>
    [HttpGet]
    public ActionResult GetAll(
        [FromQuery] int pageSize = 5,
        [FromQuery] int pageNumber = 1,
        [FromQuery] string orderBy = "positive",
        [FromQuery] string order = "desc",
        [FromQuery] string filter = "",
        [FromQuery] string keyword = ""
    )
    {
        var getAllEntitiesDTO = new GetAllEntitiesRequestDTO
        {
            PageSize = pageSize,
            PageNumber = pageNumber,
            OrderBy = orderBy,
            Order = order,
            Filter = filter,
            Keyword = keyword,
        };

        var votes = this.service.GetAll(getAllEntitiesDTO);

        return this.Ok(new { count = votes.Count, votes });
    }
}