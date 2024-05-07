using System.Net;
using JalaU.CIS_API.System.Api.Restful;
using JalaU.CIS_API.System.Core.Domain;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace JalaU.CIS_API.Tests.Api.Restful;

[TestFixture]
public class IdeaControllerTests
{
    private Mock<IService<Idea>> _serviceMock;
    private IdeaController _controller;

    [SetUp]
    public void Setup()
    {
        _serviceMock = new Mock<IService<Idea>>();
        _controller = new IdeaController(_serviceMock.Object);
    }

    [Test]
    public void SaveIdea_ReturnsObjectResultWithCreatedStatus()
    {
        var ideaRequestDTO = new IdeaRequestDTO
        {
            Title = "Test Idea",
            Description = "This is a test idea",
            TopicId = Guid.NewGuid()
        };
        var savedIdea = new Idea
        {
            Id = Guid.NewGuid(),
            Title = ideaRequestDTO.Title,
            Description = ideaRequestDTO.Description,
            Date = DateTime.Now,
            UserId = Guid.NewGuid(),
            TopicId = ideaRequestDTO.TopicId
        };
        _serviceMock.Setup(s => s.Save(ideaRequestDTO)).Returns(savedIdea);

        var result = _controller.SaveIdea(ideaRequestDTO);

        Assert.That(result, Is.InstanceOf<ObjectResult>());
        var objectResult = result as ObjectResult;
        Assert.Multiple(() =>
        {
            Assert.That(objectResult!.StatusCode, Is.EqualTo((int)HttpStatusCode.Created));
            Assert.That(objectResult.Value, Is.SameAs(savedIdea));
        });
    }

    [Test]
    public void GetIdeaByCriteria_ReturnsOkAndIdea()
    {
        var ideaId = Guid.NewGuid();
        var idea = new Idea
        {
            Id = ideaId,
            Title = "Test Idea",
            Description = "This is a test idea",
            Date = DateTime.Now,
            UserId = Guid.NewGuid(),
            TopicId = Guid.NewGuid()
        };
        _serviceMock.Setup(s => s.GetByCriteria("id", ideaId.ToString())).Returns(idea);

        var result = _controller.GetIdeaByCriteria(ideaId.ToString());

        Assert.That(result, Is.InstanceOf<OkObjectResult>());
        var okResult = result as OkObjectResult;
        Assert.That(okResult!.Value, Is.SameAs(idea));
    }

    [Test]
    public void DeleteIdea_ReturnsOkAndIdea()
    {
        var ideaId = Guid.NewGuid();
        var deletedIdea = new Idea
        {
            Id = ideaId,
            Title = "Test Idea",
            Description = "This is a test idea",
            Date = DateTime.Now,
            UserId = Guid.NewGuid(),
            TopicId = Guid.NewGuid()
        };
        _serviceMock.Setup(s => s.DeleteById(ideaId.ToString())).Returns(deletedIdea);

        var result = _controller.DeleteIdea(ideaId.ToString());

        Assert.That(result, Is.InstanceOf<OkObjectResult>());
        var okResult = result as OkObjectResult;
        Assert.That(okResult!.Value, Is.SameAs(deletedIdea));
    }

    [Test]
    public void GetAll_ReturnsOkAndIdeas()
    {
        var ideas = new List<Idea>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Title = "Test Idea 1",
                Description = "This is the first test idea",
                Date = DateTime.Now,
                UserId = Guid.NewGuid(),
                TopicId = Guid.NewGuid()
            },
            new()
            {
                Id = Guid.NewGuid(),
                Title = "Test Idea 2",
                Description = "This is the second test idea",
                Date = DateTime.Now,
                UserId = Guid.NewGuid(),
                TopicId = Guid.NewGuid()
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
        _serviceMock.Setup(s => s.GetAll(It.IsAny<GetAllEntitiesRequestDTO>())).Returns(ideas);

        var result = _controller.GetAll(
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
        Assert.That(resultValue["count"], Is.EqualTo(ideas.Count));
        CollectionAssert.AreEqual(
            ideas,
            (global::System.Collections.IEnumerable)resultValue["ideas"]
        );
    }

    [Test]
    public void UpdateIdea_ReturnsReturnsOkAndIdea()
    {
        var ideaId = Guid.NewGuid();
        var updateIdeaRequestDTO = new UpdateIdeaRequestDTO
        {
            Title = "Updated Test Idea",
            Description = "This is the updated test idea",
        };
        var updatedIdea = new Idea
        {
            Id = ideaId,
            Title = updateIdeaRequestDTO.Title,
            Description = updateIdeaRequestDTO.Description,
            Date = DateTime.Now,
            UserId = Guid.NewGuid(),
            TopicId = Guid.NewGuid(),
        };
        _serviceMock
            .Setup(s => s.Update(updateIdeaRequestDTO, ideaId.ToString()))
            .Returns(updatedIdea);

        var result = _controller.UpdateIdea(updateIdeaRequestDTO, ideaId.ToString());

        Assert.That(result, Is.InstanceOf<OkObjectResult>());
        var okResult = result as OkObjectResult;
        Assert.That(okResult!.Value, Is.SameAs(updatedIdea));
    }
}
