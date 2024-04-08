using JalaU.CIS_API.System.Core.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace JalaU.CIS_API.System.Api.Restful;

[ApiController]
[Route("cis-api/v1/topics")]
public class TopicController(ILogger<TopicController> logger, IService<Topic> service)
    : ControllerBase
{
    private readonly IService<Topic> _service = service;
    private readonly ILogger<TopicController> _logger = logger;

    [HttpGet]
    public ActionResult GetTopics()
    {
        Dictionary<string, object> topicsMap = [];
        List<Topic> topics = [];
        topics.Add(
            new Topic
            {
                Id = Guid.NewGuid(),
                Title = "Help, I can't sleep",
                Description = "Long life to Software Dev 3",
                Date = DateTime.Now,
                Labels = ["#IFeelSleepy"],
                UserId = Guid.NewGuid()
            }
        );
        topicsMap.Add("count", topics.Count);
        topicsMap.Add("topics", topics);

        return Ok(topicsMap);
    }
}
