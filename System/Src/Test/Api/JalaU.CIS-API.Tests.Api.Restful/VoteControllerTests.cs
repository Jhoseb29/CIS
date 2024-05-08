using System.Net;
using JalaU.CIS_API.System.Api.Restful;
using JalaU.CIS_API.System.Core.Domain;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace JalaU.CIS_API.Tests.Api.Restful;

public class VoteControllerTests
{
    private Mock<IService<Vote>> _serviceMock;
    private VoteController _controller;

    [SetUp]
    public void Setup()
    {
        _serviceMock = new Mock<IService<Vote>>();
        _controller = new VoteController(_serviceMock.Object);
    }

    [Test]
    public void SaveVote_ReturnsObjectResultWithCreatedStatus()
    {
        var voteRequestDTO = new VoteRequestDTO { Positive = true, IdeaId = Guid.NewGuid(), };
        var savedVote = new Vote
        {
            Id = Guid.NewGuid().ToString(),
            Positive = voteRequestDTO.Positive,
            UserId = Guid.NewGuid().ToString(),
            IdeaId = voteRequestDTO.IdeaId.ToString()
        };
        _serviceMock.Setup(s => s.Save(voteRequestDTO)).Returns(savedVote);

        var result = _controller.SaveVote(voteRequestDTO);

        Assert.That(result, Is.InstanceOf<ObjectResult>());
        var objectResult = result as ObjectResult;
        Assert.Multiple(() =>
        {
            Assert.That(objectResult!.StatusCode, Is.EqualTo((int)HttpStatusCode.Created));
            Assert.That(objectResult.Value, Is.SameAs(savedVote));
        });
    }

    [Test]
    public void GetVoteByCriteria_ReturnsOkAndVote()
    {
        var voteId = Guid.NewGuid();
        var vote = new Vote
        {
            Id = voteId.ToString(),
            Positive = true,
            UserId = Guid.NewGuid().ToString(),
            IdeaId = Guid.NewGuid().ToString(),
        };
        _serviceMock.Setup(s => s.GetByCriteria("id", voteId.ToString())).Returns(vote);

        var result = _controller.GetVoteById(voteId.ToString());

        Assert.That(result, Is.InstanceOf<OkObjectResult>());
        var okResult = result as OkObjectResult;
        Assert.That(okResult!.Value, Is.SameAs(vote));
    }

    [Test]
    public void DeleteVote_ReturnsOkAndVote()
    {
        var voteId = Guid.NewGuid();
        var deletedVote = new Vote
        {
            Id = voteId.ToString(),
            Positive = false,
            UserId = Guid.NewGuid().ToString(),
            IdeaId = Guid.NewGuid().ToString(),
        };
        _serviceMock.Setup(s => s.DeleteById(voteId.ToString())).Returns(deletedVote);

        var result = _controller.DeleteVote(voteId.ToString());

        Assert.That(result, Is.InstanceOf<OkObjectResult>());
        var okResult = result as OkObjectResult;
        Assert.That(okResult!.Value, Is.SameAs(deletedVote));
    }

    [Test]
    public void GetAll_ReturnsOkAndVotes()
    {
        var votes = new List<Vote>
        {
            new()
            {
                Id = Guid.NewGuid().ToString(),
                Positive = false,
                UserId = Guid.NewGuid().ToString(),
                IdeaId = Guid.NewGuid().ToString()
            },
            new()
            {
                Id = Guid.NewGuid().ToString(),
                Positive = true,
                UserId = Guid.NewGuid().ToString(),
                IdeaId = Guid.NewGuid().ToString()
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
        _serviceMock.Setup(s => s.GetAll(It.IsAny<GetAllEntitiesRequestDTO>())).Returns(votes);

        var result = _controller.GetVotes(
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
        Assert.That(resultValue["count"], Is.EqualTo(votes.Count));
        CollectionAssert.AreEqual(
            votes,
            (global::System.Collections.IEnumerable)resultValue["votes"]
        );
    }

    [Test]
    public void UpdateVote_ReturnsReturnsOkAndVote()
    {
        var voteId = Guid.NewGuid();
        var updateVoteRequestDTO = new UpdateVoteRequestDTO { Positive = true, };
        var updatedVote = new Vote
        {
            Id = voteId.ToString(),
            Positive = true,
            UserId = Guid.NewGuid().ToString(),
            IdeaId = Guid.NewGuid().ToString(),
        };
        _serviceMock
            .Setup(s => s.Update(updateVoteRequestDTO, voteId.ToString()))
            .Returns(updatedVote);

        var result = _controller.UpdateVote(updateVoteRequestDTO, voteId.ToString());

        Assert.That(result, Is.InstanceOf<OkObjectResult>());
        var okResult = result as OkObjectResult;
        Assert.That(okResult!.Value, Is.SameAs(updatedVote));
    }
}
