//-----------------------------------------------------------------------
// <copyright file="VoteController.cs" company="Jala University">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using global::System.Net;
using JalaU.CIS_API.System.Core.Application;
using JalaU.CIS_API.System.Core.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace JalaU.CIS_API.System.Api.Restful;

/// <summary>
/// Controller for managing votes.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="VoteController"/> class.
/// </remarks>
/// <param name="logger">The logger instance for logging.</param>
/// <param name="service">The service instance for managing votes.</param>
[ApiController]
[Route("cis-api/v1/votes")]
public class VoteController(ILogger<VoteController> logger, IService<Vote> service) : ControllerBase
{
    private readonly IService<Vote> service = service;
    private readonly ILogger<VoteController> logger = logger;

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
        List<object> errorList = [];
        Dictionary<string, object> errorMap = [];
        try
        {
            var topic = this.service.DeleteById(voteId);
            return this.Ok(topic);
        }
        catch (EntityNotFoundException notFoundException)
        {
            errorList.Add(
                new MessageLogDTO((int)HttpStatusCode.NotFound, notFoundException.Message)
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
        List<object> errorList = [];
        Dictionary<string, object> errorMap = [];
        try
        {
            var updatedVote = this.service.Update(voteRequestDto, voteId);
            return this.Ok(updatedVote);
        }
        catch (EntityNotFoundException notFoundException)
        {
            errorList.Add(
                new MessageLogDTO((int)HttpStatusCode.NotFound, notFoundException.Message)
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
}
