//-----------------------------------------------------------------------
// <copyright file="IdeaController.cs" company="Jala University">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace JalaU.CIS_API.System.Api.Restful;

using global::System.Net;
using JalaU.CIS_API.System.Core.Application;
using JalaU.CIS_API.System.Core.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

/// <summary>
/// Controller for managing ideas.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="IdeaController"/> class.
/// </remarks>
/// <param name="service">The service instance for managing ideas.</param>
[Authorize]
[ApiController]
[Route("cis-api/v1/ideas")]
public class IdeaController(IService<Idea> service) : ControllerBase
{
    private readonly IService<Idea> service = service;

    /// <summary>
    /// Saves a topic by its ID using HTTP DELETE method.
    /// </summary>
    /// <param name="ideaRequestDTO">The ID of the topic to be deleted.</param>
    /// <returns>
    /// An HTTP 200 OK response with the updated topic in the body.
    /// An HTTP 400 Bad Request response with all error details.
    /// </returns>
    [HttpPost]
    public ActionResult SaveIdea(IdeaRequestDTO ideaRequestDTO)
    {
        Idea savedIdea = this.service.Save(ideaRequestDTO);

        return this.StatusCode((int)HttpStatusCode.Created, savedIdea);
    }

    /// <summary>
    /// Retrieves an idea by its ID using HTTP GET method.
    /// </summary>
    /// <param name="ideaId">The ID of the idea to retrieve.</param>
    /// <returns>
    /// An HTTP 200 OK response with the retrieved idea in the body if found.
    /// An HTTP 404 Not Found response if the idea is not found.
    /// </returns>
    [HttpGet("{ideaId}")]
    public ActionResult GetIdeaByCriteria(string ideaId)
    {
        var idea = this.service.GetByCriteria("id", ideaId);

        return this.Ok(idea);
    }

    /// <summary>
    /// Deletes an idea by its ID using HTTP DELETE method.
    /// </summary>
    /// <param name="ideaId">The ID of the idea to be deleted.</param>
    /// <returns>
    /// An HTTP 200 OK response with the updated idea in the body.
    /// An HTTP 400 Bad Request response with all error details.
    /// </returns>
    [HttpDelete("{ideaId}")]
    public ActionResult DeleteIdea(string ideaId)
    {
        var idea = this.service.DeleteById(ideaId);
        return this.Ok(idea);
    }

    /// <summary>
    /// Retrieves a paginated list of Ideas.
    /// </summary>
    /// <param name="pageSize">Optional. The number of Idea to include in a page.</param>
    /// <param name="pageNumber">Optional. The page number to retrieve.</param>
    /// <param name="orderBy"> orderBy. </param>
    /// <param name="order"> order.</param>
    /// <param name="filter"> filter.</param>
    /// <param name="keyword">keyword.</param>
    /// <returns>
    /// An action result containing a dictionary with information about Idea.
    /// </returns>
    [HttpGet]
    public ActionResult GetAll(
        [FromQuery] int pageSize = 5,
        [FromQuery] int pageNumber = 1,
        [FromQuery] string orderBy = "title",
        [FromQuery] string order = "asc",
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

        var ideas = this.service.GetAll(getAllEntitiesDTO);

        return this.Ok(new { count = ideas.Count, ideas });
    }

    /// <summary>
    /// Updates a idea by its ID using HTTP PUT method.
    /// </summary>
    /// <param name="updateIdeaRequestDTO">The DTO (Data Transfer Object) containing updated idea information.</param>
    /// <param name="ideaId">The ID of the idea to be updated.</param>
    /// <returns>
    /// An HTTP 200 OK response with the updated idea in the body.
    /// An HTTP 400 Bad Request response with all error details.
    /// </returns>
    [HttpPut("{ideaId}")]
    public ActionResult UpdateIdea([FromBody]UpdateIdeaRequestDTO updateIdeaRequestDTO, string ideaId)
    {
        var idea = this.service.Update(updateIdeaRequestDTO, ideaId);
        return this.Ok(idea);
    }
}
