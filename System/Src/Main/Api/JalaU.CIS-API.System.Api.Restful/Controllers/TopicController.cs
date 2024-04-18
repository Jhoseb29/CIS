//-----------------------------------------------------------------------
// <copyright file="TopicController.cs" company="Jala University">
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
/// Controller for managing topics.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="TopicController"/> class.
/// </remarks>
/// <param name="logger">The logger instance for logging.</param>
/// <param name="service">The service instance for managing topics.</param>
[ApiController]
[Route("cis-api/v1/topics")]
public class TopicController(ILogger<TopicController> logger, IService<Topic> service)
    : ControllerBase
{
    private readonly IService<Topic> service = service;
    private readonly ILogger<TopicController> logger = logger;

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
        [FromQuery] int? pageSize = 5,
        [FromQuery] int pageNumber = 1,
        [FromQuery] string orderBy = "title",
        [FromQuery] string order = "asc",
        [FromQuery] string filter = "",
        [FromQuery] string keyword = ""
    )
    {
        try
        {
            var topics = this.GetFilteredAndOrderedTopics(filter, keyword, orderBy, order);
            var paginatedTopics = this.GetPaginatedTopics(topics, pageSize, pageNumber);

            if (paginatedTopics.Count == 0)
            {
                return this.NotFound();
            }

            return this.Ok(new { count = paginatedTopics.Count, topics = paginatedTopics });
        }
        catch (ArgumentException ex)
        {
            return this.BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, "An error occurred while retrieving topics.");
            return this.StatusCode((int)HttpStatusCode.InternalServerError);
        }
    }

    /// <summary>
    /// Retrieves a topic by criteria.
    /// </summary>
    /// <param name="topicId"> value To Search. </param>
    /// <returns>topic.</returns>
    [HttpGet("{topicId}")]
    public ActionResult GetTopicByCriteria(string topicId)
    {
        try
        {
            Topic? topic = this.service.GetByCriteria("id", topicId);
            return this.Ok(topic);
        }
        catch (EntityNotFoundException entityNotFoundException)
        {
            return this.NotFound(entityNotFoundException.Message);
        }
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
        List<object> errorList = [];
        Dictionary<string, object> errorMap = [];
        try
        {
            var updatedTopic = this.service.Update(topicRequestDto, topicId);
            return this.Ok(updatedTopic);
        }
        catch (EntityNotFoundException notFoundException)
        {
            errorList.Add(
                new MessageLogDTO((int)HttpStatusCode.NotFound, notFoundException.Message)
            );
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
        try
        {
            var topic = this.service.DeleteById(topicId);
            return this.Ok(topic);
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, "An error occurred while filtering topics.");
            return this.StatusCode((int)HttpStatusCode.InternalServerError);
        }
    }

    private List<Topic> GetFilteredAndOrderedTopics(
        string filter,
        string keyword,
        string orderBy,
        string order
    )
    {
        var topics =
            string.IsNullOrEmpty(filter) || string.IsNullOrEmpty(keyword)
                ? this.service.GetAll()
                : this.service.FilterEntities(filter, keyword);

        if (!string.IsNullOrEmpty(orderBy))
        {
            if (
                orderBy.Equals("title", StringComparison.CurrentCultureIgnoreCase)
                || orderBy.Equals("date", StringComparison.CurrentCultureIgnoreCase)
            )
            {
                if (
                    !order.Equals("asc", StringComparison.CurrentCultureIgnoreCase)
                    && !order.Equals("desc", StringComparison.CurrentCultureIgnoreCase)
                )
                {
                    throw new ArgumentException(
                        "Invalid order parameter. Supported values are 'asc' and 'desc'."
                    );
                }

                var topicSorter = new GenericSorter<Topic>();
                topics = topicSorter.Sort(
                    topics,
                    t =>
                        orderBy.Equals("title", StringComparison.CurrentCultureIgnoreCase)
                            ? t.Title
                            : t.Date,
                    order
                );
            }
            else
            {
                throw new ArgumentException(
                    "Invalid orderBy parameter. Supported values are 'title' and 'date'."
                );
            }
        }

        return topics;
    }

    private List<Topic> GetPaginatedTopics(List<Topic> topics, int? pageSize, int pageNumber)
    {
        int startIndex = (pageNumber - 1) * (pageSize ?? topics.Count);
        int endIndex = Math.Min(startIndex + (pageSize ?? topics.Count), topics.Count);

        return topics.GetRange(startIndex, endIndex - startIndex);
    }
}
