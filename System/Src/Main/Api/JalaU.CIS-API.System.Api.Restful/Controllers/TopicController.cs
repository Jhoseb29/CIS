﻿//-----------------------------------------------------------------------
// <copyright file="TopicController.cs" company="Jala University">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace JalaU.CIS_API.System.Api.Restful;

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
public class TopicController(ILogger<TopicController> logger, IService<Topic> service): ControllerBase
{
    private readonly IService<Topic> service = service;
    private readonly ILogger<TopicController> logger = logger;

    /// <summary>
    /// Retrieves a list of topics.
    /// </summary>
    /// <returns>An action result containing a dictionary with information about topics.</returns>
    [HttpGet]
    public ActionResult GetTopics()
    {
        Dictionary<string, object> topicsMap = [];
        List<Topic> topics =
        [
            new Topic
            {
                Id = Guid.NewGuid(),
                Title = "Help, I can't sleep",
                Description = "Long life to Software Dev 3",
                Date = DateTime.Now,
                Labels = ["#IFeelSleepy"],
                UserId = Guid.NewGuid(),
            },
        ];

        topicsMap.Add("count", topics.Count);
        topicsMap.Add("topics", topics);

        return this.Ok(topicsMap);
    }

    [HttpPost("Create")]
    public ActionResult CreateTopic(Topic topic)
    {
        Dictionary<string, object> topicsMap = [];
        List<Topic> topics =
        [
            new Topic
            {
                Id = Guid.NewGuid(),
                Title = "Help, I can't sleep",
                Description = "Long life to Software Dev 3",
                Date = DateTime.Now,
                Labels = ["#IFeelSleepy"],
                UserId = Guid.NewGuid(),
            },
        ];

        topicsMap.Add("count", topics.Count);
        topicsMap.Add("topics", topics);

        return this.Ok(topicsMap);
    }
}
