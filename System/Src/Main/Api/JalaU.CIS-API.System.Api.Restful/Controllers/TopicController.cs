//-----------------------------------------------------------------------
// <copyright file="TopicController.cs" company="Jala University">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace JalaU.CIS_API.System.Api.Restful;

using global::System.Net;
using JalaU.CIS_API.System.Core.Application;
using JalaU.CIS_API.System.Core.Domain;
using Microsoft.AspNetCore.Http.HttpResults;
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
    /// Retrieves a list of topics.
    /// </summary>
    /// <param name="pageSize">The number of topics per page. If null, all topics are returned.</param>
    /// <param name="pageNumber">The page number to retrieve. Default is 1.</param>
    /// <returns>An action result containing a dictionary with information about topics.</returns>
    [HttpGet]
    public ActionResult GetTopics([FromQuery] int? pageSize, [FromQuery] int pageNumber = 1)
    {
        if (pageNumber != 1)
        {
            pageNumber = 1;
        }

        // Method to inject data without injecting directly on the database, for proofing purposes.
        // List<Topic> topicRepo = this.GenerateSampleTopics(15);
        List<Topic> topicRepo = this.service.GetAll();

        int startIndex = (pageNumber - 1) * (pageSize ?? topicRepo.Count); // If pageSize is null, show all records.
        int endIndex = Math.Min(startIndex + (pageSize ?? topicRepo.Count), topicRepo.Count);

        List<Topic> topicList = topicRepo.GetRange(startIndex, endIndex - startIndex);

        Dictionary<string, object> topicsMap = new ()
        {
            { "count", topicList.Count },
            { "topics", topicList },
        };

        if (topicList.Count == 0)
        {
            return this.NotFound();
        }
        else
        {
            return this.Ok(topicsMap);
        }
    }

    /// <summary>
    /// Retrieves a single topic by its ID.
    /// </summary>
    /// <param name="topicId">The ID of the topic to retrieve.</param>
    /// <returns>An action result containing the retrieved topic,
    /// or NotFound if no topic is found with the specified ID.</returns>
    [HttpGet("{topicId}")]
    public ActionResult GetTopicById(Guid topicId)
    {
        Topic topic = this.service.GetById(topicId);

        if (topic == null)
        {
            return this.NotFound();
        }
        else
        {
            return this.Ok(topic);
        }
    }

    /// <summary>
    /// Retrieves a single topic by its ID.
    /// </summary>
    /// <param name="title">The title of the topic to retrieve.</param>
    /// <returns>An action result containing the retrieved topic,
    /// or NotFound if no topic is found with the specified ID.</returns>
    [HttpGet("title")]
    public ActionResult GetTopicByTitle(string title)
    {
        Topic topic = this.service.GetByTitle(title);

        if (topic == null)
        {
            return this.NotFound();
        }
        else
        {
            return this.Ok(topic);
        }
    }

    /// <summary>
    /// Filter the Topics by two filters.
    /// </summary>
    /// <param name="filter">The type of filter that will be applied.</param>
    /// <param name="keyword">The key word to apply the filter.</param>
    /// <returns>The entity with the characteristics asked.</returns>
    [HttpGet("filter")]
    public ActionResult FilterTopics(string filter, string keyword)
        {
            try
            {
                List<Topic> filteredTopics = this.service.FilterEntities(filter, keyword);

                if (filteredTopics.Count == 0)
                {
                    return this.NotFound();
                }
                else
                {
                    return this.Ok(filteredTopics);
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "An error occurred while filtering topics.");
                return this.StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

    /// <summary>
    /// Simulate a list of topics like it were came from a database.
    /// </summary>
    /// <returns>A bunch of topics, depending on how much we specify on the call: List.<Topic> topicRepo = this.GenerateSampleTopics(15);.</returns>
    private List<Topic> GenerateSampleTopics(int count)
    {
        List<Topic> topics = new List<Topic>();

        for (int i = 0; i < count; i++)
        {
            topics.Add(new Topic
            {
                Id = Guid.NewGuid(),
                Title = $"Topic Title {i + 1}",
                Description = $"Topic Description {i + 1}",
                Date = DateTime.Now.AddDays(-i),
                Labels = new List<string> { $"Label {i + 1}" },
                UserId = Guid.NewGuid(),
            });
        }

        return topics;
    }
}
