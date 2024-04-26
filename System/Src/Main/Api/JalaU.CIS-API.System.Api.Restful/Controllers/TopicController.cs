//-----------------------------------------------------------------------
// <copyright file="TopicController.cs" company="Jala University">
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
/// Controller for managing topics.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="TopicController"/> class.
/// </remarks>
/// <param name="service">The service instance for managing topics.</param>
[Authorize]
[ApiController]
[Route("cis-api/v1/topics")]
public class TopicController(IService<Topic> service) : ControllerBase
{
    private readonly IService<Topic> service = service;

    /// <summary>
    /// Saves a topic by its ID using HTTP DELETE method.
    /// </summary>
    /// <param name="topic">The ID of the topic to be deleted.</param>
    /// <returns>
    /// An HTTP 200 OK response with the updated topic in the body.
    /// An HTTP 400 Bad Request response with all error details.
    /// </returns>
    [HttpPost]
    public ActionResult SaveTopic(TopicRequestDTO topic)
    {
        Topic savedTopic = this.service.Save(topic);

        return this.StatusCode((int)HttpStatusCode.Created, savedTopic);
    }

    /// <summary>
    /// Retrieves a paginated list of topics.
    /// </summary>
    /// <param name="pageSize">Optional. The number of topics to include in a page.</param>
    /// <param name="pageNumber">Optional. The page number to retrieve.</param>
    /// <param name="orderBy"> orderBy. </param>
    /// <param name="order"> order.</param>
    /// <param name="filter"> filter.</param>
    /// <param name="keyword">keyword.</param>
    /// <returns>
    /// An action result containing a dictionary with information about topics.
    /// </returns>
    [HttpGet]
    public ActionResult GetTopics(
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

        var topics = this.service.GetAll(getAllEntitiesDTO);

        return this.Ok(new { count = topics.Count, topics });
    }

    /// <summary>
    /// Retrieves a topic by criteria.
    /// </summary>
    /// <param name="topicId"> value To Search. </param>
    /// <returns>topic.</returns>
    [HttpGet("{topicId}")]
    public ActionResult GetTopicById(string topicId)
    {
        Topic? topic = this.service.GetByCriteria("id", topicId);

        return this.Ok(topic);
    }

    /// <summary>
    /// Updates a topic by its ID using HTTP PUT method.
    /// </summary>
    /// <param name="topicRequestDto">The DTO (Data Transfer Object) containing updated topic information.</param>
    /// <param name="topicId">The ID of the topic to be updated.</param>
    /// <returns>
    /// An HTTP 200 OK response with the updated topic in the body.
    /// An HTTP 400 Bad Request response with all error details.
    /// </returns>
    [HttpPut("{topicId}")]
    public ActionResult UpdateTopicById([FromBody] TopicRequestDTO topicRequestDto, string topicId)
    {
        var updatedTopic = this.service.Update(topicRequestDto, topicId);

        return this.Ok(updatedTopic);
    }

    /// <summary>
    /// Deletes a topic by its ID using HTTP DELETE method.
    /// </summary>
    /// <param name="topicId">The ID of the topic to be deleted.</param>
    /// <returns>
    /// An HTTP 200 OK response with the updated topic in the body.
    /// An HTTP 400 Bad Request response with all error details.
    /// </returns>
    [HttpDelete("{topicId}")]
    public ActionResult DeleteTopic(string topicId)
    {
        var topic = this.service.DeleteById(topicId);

        return this.Ok(topic);
    }
}
