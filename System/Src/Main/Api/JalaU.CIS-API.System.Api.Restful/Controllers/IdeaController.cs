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
    /// Saves a topic by its ID using HTTP DELETE method.
    /// </summary>
    /// <param name="topic">The ID of the topic to be deleted.</param>
    /// <returns>
    /// An HTTP 200 OK response with the updated topic in the body.
    /// An HTTP 400 Bad Request response with all error details.
    /// </returns>
    [HttpPost]
    public ActionResult SaveTopic(IdeaRequestDTO topic)
    {
        List<object> errorList = [];
        Dictionary<string, object> errorMap = [];
        try
        {
            Topic savedTopic = this.service.Save(topic);

            return this.StatusCode((int)HttpStatusCode.Created, savedTopic);
        }
        catch (DuplicateEntryException duplicateEntryException)
        {
            errorList.Add(
                new MessageLogDTO((int)HttpStatusCode.Conflict, duplicateEntryException.Message)
            );
        }
        catch (WrongDataException wrongDataException)
        {
            errorList.AddRange(wrongDataException.MessageLogs);
        }
        catch (Exception exception)
        {
            errorList.Add(
                new MessageLogDTO((int)HttpStatusCode.InternalServerError, exception.Message)
            );
        }

        errorMap.Add("errors", errorList);
        return this.BadRequest(errorMap);
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
}
