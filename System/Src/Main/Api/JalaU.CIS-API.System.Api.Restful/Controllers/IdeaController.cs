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
}
