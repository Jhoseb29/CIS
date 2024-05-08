using System.Net;
using JalaU.CIS_API.System.Api.Restful;
using JalaU.CIS_API.System.Core.Domain;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace JalaU.CIS_API.Tests.Api.Restful;

public class TopicControllerTests
{
    private Mock<IService<Topic>> _serviceMock;
    private TopicController _controller;

    [SetUp]
    public void Setup()
    {
        _serviceMock = new Mock<IService<Topic>>();
        _controller = new TopicController(_serviceMock.Object);
    }

    [Test]
    public void SaveTopic_ReturnsObjectResultWithCreatedStatus()
    {
        var topicRequestDTO = new TopicRequestDTO
        {
            Title = "Test Topic",
            Description = "This is a test topic.",
            Labels = ["save", "topic"],
        };
        var savedTopic = new Topic
        {
            Id = Guid.NewGuid().ToString(),
            Title = topicRequestDTO.Title,
            Description = topicRequestDTO.Description,
            Date = DateTime.Now,
            UserId = Guid.NewGuid().ToString(),
            Labels = ["save", "topic"],
        };
        _serviceMock.Setup(s => s.Save(topicRequestDTO)).Returns(savedTopic);

        var result = _controller.SaveTopic(topicRequestDTO);

        Assert.That(result, Is.InstanceOf<ObjectResult>());
        var objectResult = result as ObjectResult;
        Assert.Multiple(() =>
        {
            Assert.That(objectResult!.StatusCode, Is.EqualTo((int)HttpStatusCode.Created));
            Assert.That(objectResult.Value, Is.SameAs(savedTopic));
        });
    }

    [Test]
    public void GetTopicByCriteria_ReturnsOkAndTopic()
    {
        var topicId = Guid.NewGuid();
        var topic = new Topic
        {
            Id = topicId.ToString(),
            Title = "Test Idea",
            Description = "This is a test idea",
            Date = DateTime.Now,
            UserId = Guid.NewGuid().ToString(),
            Labels = ["get", "topic"],
        };
        _serviceMock.Setup(s => s.GetByCriteria("id", topicId.ToString())).Returns(topic);

        var result = _controller.GetTopicById(topicId.ToString());

        Assert.That(result, Is.InstanceOf<OkObjectResult>());
        var okResult = result as OkObjectResult;
        Assert.That(okResult!.Value, Is.SameAs(topic));
    }

    [Test]
    public void DeleteTopic_ReturnsOkAndTopic()
    {
        var topicId = Guid.NewGuid();
        var deletedTopic = new Topic
        {
            Id = topicId.ToString(),
            Title = "Test Idea",
            Description = "This is a test idea",
            Date = DateTime.Now,
            UserId = Guid.NewGuid().ToString(),
            Labels = ["delete", "topic"],
        };
        _serviceMock.Setup(s => s.DeleteById(topicId.ToString())).Returns(deletedTopic);

        var result = _controller.DeleteTopic(topicId.ToString());

        Assert.That(result, Is.InstanceOf<OkObjectResult>());
        var okResult = result as OkObjectResult;
        Assert.That(okResult!.Value, Is.SameAs(deletedTopic));
    }

    [Test]
    public void GetAll_ReturnsOkAndTopics()
    {
        var topics = new List<Topic>
        {
            new()
            {
                Id = Guid.NewGuid().ToString(),
                Title = "Test Topic 1",
                Description = "This is the first test Topic",
                Date = DateTime.Now,
                UserId = Guid.NewGuid().ToString(),
                Labels = ["get", "topics"],
            },
            new()
            {
                Id = Guid.NewGuid().ToString(),
                Title = "Test Topic 2",
                Description = "This is the second test Topic",
                Date = DateTime.Now,
                UserId = Guid.NewGuid().ToString(),
                Labels = ["get", "topics"],
            }
        };
        var getAllEntitiesDTO = new GetAllEntitiesRequestDTO
        {
            PageSize = 10,
            PageNumber = 1,
            OrderBy = "title",
            Order = "asc",
            Filter = "",
            Keyword = ""
        };
        _serviceMock.Setup(s => s.GetAll(It.IsAny<GetAllEntitiesRequestDTO>())).Returns(topics);

        var result = _controller.GetTopics(
            getAllEntitiesDTO.PageSize,
            getAllEntitiesDTO.PageNumber,
            getAllEntitiesDTO.OrderBy,
            getAllEntitiesDTO.Order,
            getAllEntitiesDTO.Filter,
            getAllEntitiesDTO.Keyword
        );

        Assert.That(result, Is.InstanceOf<OkObjectResult>());
        var okResult = result as OkObjectResult;
        var resultValue = okResult!.Value as Dictionary<string, object>;
        Assert.That(resultValue, Is.Not.Null);
        Assert.That(resultValue["count"], Is.EqualTo(topics.Count));
        CollectionAssert.AreEqual(
            topics,
            (global::System.Collections.IEnumerable)resultValue["topics"]
        );
    }

    [Test]
    public void UpdateTopic_ReturnsReturnsOkAndTopic()
    {
        var topicId = Guid.NewGuid();
        var fieldOfTopicToUpdate = new TopicRequestDTO
        {
            Title = "Updated Test Topic",
            Description = "This is the updated test Topic",
            Labels = ["update", "topic"],
        };
        var updatedTopic = new Topic
        {
            Id = topicId.ToString(),
            Title = fieldOfTopicToUpdate.Title,
            Description = fieldOfTopicToUpdate.Description,
            Date = DateTime.Now,
            UserId = Guid.NewGuid().ToString(),
            Labels = ["update", "topic"]
        };
        _serviceMock
            .Setup(s => s.Update(fieldOfTopicToUpdate, topicId.ToString()))
            .Returns(updatedTopic);

        var result = _controller.UpdateTopicById(fieldOfTopicToUpdate, topicId.ToString());

        Assert.That(result, Is.InstanceOf<OkObjectResult>());
        var okResult = result as OkObjectResult;
        Assert.That(okResult!.Value, Is.SameAs(updatedTopic));
    }
}
