//-----------------------------------------------------------------------
// <copyright file="IdeaController.cs" company="Jala University">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace JalaU.CIS_API.System.Api.Restful;

using global::System.Net;
using JalaU.CIS_API.System.Core.Application;
using JalaU.CIS_API.System.Core.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

/// <summary>
/// Controller for managing ideas.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="IdeaController"/> class.
/// </remarks>
/// <param name="logger">The logger instance for logging.</param>
/// <param name="service">The service instance for managing ideas.</param>
[ApiController]
[Route("cis-api/v1/ideas")]
public class IdeaController(ILogger<IdeaController> logger, IService<Idea> service) : ControllerBase
{
    private readonly IService<Idea> service = service;
    private readonly ILogger<IdeaController> logger = logger;

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
        try
        {
            var idea = this.service.DeleteById(ideaId);
            return this.Ok(idea);
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, "Error deleting idea.");
            return this.StatusCode((int)HttpStatusCode.InternalServerError);
        }
    }

    /// <summary>
    /// Retrieves an idea by its ID or title using HTTP GET method.
    /// </summary>
    /// <param name="ideaIdOrTitle">The ID or title of the idea to retrieve.</param>
    /// <returns>
    /// An HTTP 200 OK response with the retrieved idea in the body if found.
    /// An HTTP 404 Not Found response if the idea is not found.
    /// </returns>
    [HttpGet("{ideaIdOrTitle}")]
    public IActionResult GetIdeaByCriteria(string ideaIdOrTitle)
    {
        try
        {
            if (Guid.TryParse(ideaIdOrTitle, out Guid _))
            {
                Idea? ideaById = this.service.GetByCriteria("id", ideaIdOrTitle);
                if (ideaById != null)
                {
                    return this.Ok(ideaById);
                }
            }

            Idea? ideaByTitle = this.service.GetByCriteria("title", ideaIdOrTitle);
            if (ideaByTitle != null)
            {
                return this.Ok(ideaByTitle);
            }
            return this.NotFound("Idea not found.");
        }
        catch (EntityNotFoundException ex)
        {
            return this.NotFound(ex.Message);
        }
        catch (Exception)
        {
            return this.BadRequest("Invalid idea identifier.");
        }
    }
}
