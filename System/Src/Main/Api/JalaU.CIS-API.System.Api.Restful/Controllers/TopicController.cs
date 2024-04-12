//-----------------------------------------------------------------------
// <copyright file="TopicController.cs" company="Jala University">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace JalaU.CIS_API.System.Api.Restful;

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
    /// <returns>An action result containing a dictionary with information about topics.</returns>
    [HttpGet]
    public ActionResult GetTopics([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        List<Topic> topicRepo = this.service.GetAll();

        // <Topic> topicRepo = this.GenerateSampleTopics(15);  //Method to inject data without injecting directly on the database, for proofing purposes.

        int startIndex = (pageNumber - 1) * pageSize;
        int endIndex = Math.Min(startIndex + pageSize, topicRepo.Count);

        List<Topic> topicList = topicRepo.GetRange(startIndex, endIndex - startIndex);

        Dictionary<string, object> topicsMap = new()
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
    /// Simulate a list of topics like it were came from a databse.
    /// </summary>
    /// <returns>A bunch of topics, depending on how much we specify on the call: <Topic> topicRepo = this.GenerateSampleTopics(15);.</returns>
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
