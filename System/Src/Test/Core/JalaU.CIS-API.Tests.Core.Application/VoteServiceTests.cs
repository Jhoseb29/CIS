using JalaU.CIS_API.System.Core.Application;
using JalaU.CIS_API.System.Core.Domain;
using Moq;

namespace JalaU.CIS_API.Tests.Core.Application;

[TestFixture]
public class VoteServiceTests
{
    private Mock<IRepository<Vote>> _voteRepositoryMock;
    private Mock<IService<Idea>> _ideaServiceMock;
    private Mock<AbstractValidator<Vote>> _validatorMock;
    private Mock<EntityFilter<Vote>> _entityFilterMock;
    private VoteService _voteService;

    [SetUp]
    public void Setup()
    {
        _voteRepositoryMock = new Mock<IRepository<Vote>>();
        _ideaServiceMock = new Mock<IService<Idea>>();
        _validatorMock = new Mock<AbstractValidator<Vote>>();
        _entityFilterMock = new Mock<EntityFilter<Vote>>();

        _voteService = new VoteService(
            _voteRepositoryMock.Object,
            _ideaServiceMock.Object,
            _validatorMock.Object,
            _entityFilterMock.Object
        );
    }

    [Test]
    public void DeleteById_ReturnsDeletedVote()
    {
        var voteId = Guid.NewGuid().ToString();
        var deletedVote = new Vote()
        {
            Id = Guid.NewGuid(),
            IdeaId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Positive = true
        };
        _voteRepositoryMock
            .Setup(r => r.GetByCriteria(It.IsAny<Func<Vote, bool>>()))
            .Returns(deletedVote);
        var result = _voteService.DeleteById(voteId);

        Assert.That(result, Is.SameAs(deletedVote));
        _voteRepositoryMock.Verify(r => r.Delete(deletedVote), Times.Once);
    }

    [Test]
    public void GetAll_ReturnsListOfVotesWithFiltering()
    {
        var votes = new List<Vote>
        {
            new()
            {
                Id = Guid.NewGuid(),
                IdeaId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Positive = true
            },
            new()
            {
                Id = Guid.NewGuid(),
                IdeaId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Positive = false
            },
            new()
            {
                Id = Guid.NewGuid(),
                IdeaId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Positive = true
            }
        };
        var getAllEntitiesDTO = new GetAllEntitiesRequestDTO
        {
            PageSize = 2,
            PageNumber = 1,
            OrderBy = "positive",
            Order = "desc",
            Filter = "positive",
            Keyword = "true"
        };
        var filteredVotes = votes.Where(v => v.Positive).ToList();
        _voteRepositoryMock.Setup(r => r.GetAll()).Returns(votes.AsQueryable());
        _entityFilterMock
            .Setup(f => f.Filter(votes, getAllEntitiesDTO.Filter, getAllEntitiesDTO.Keyword))
            .Returns(filteredVotes);

        var result = _voteService.GetAll(getAllEntitiesDTO);

        CollectionAssert.AreEqual(filteredVotes, result);
    }

    [Test]
    public void GetByCriteria_ReturnsVoteById()
    {
        var voteId = Guid.NewGuid().ToString();
        var vote = new Vote()
        {
            Id = Guid.NewGuid(),
            IdeaId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Positive = true
        };
        _voteRepositoryMock.Setup(r => r.GetByCriteria(It.IsAny<Func<Vote, bool>>())).Returns(vote);

        var result = _voteService.GetByCriteria("id", voteId);

        Assert.That(result, Is.SameAs(vote));
    }

    [Test]
    public void GetByCriteria_ThrowsArgumentExceptionForInvalidField()
    {
        var invalidField = "invalidField";
        Assert.Throws<ArgumentException>(() => _voteService.GetByCriteria(invalidField, "value"));
    }

    [Test]
    public void Save_ReturnsNewVote()
    {
        GlobalVariables.UserId = Guid.NewGuid().ToString();

        var voteRequestDTO = new VoteRequestDTO { IdeaId = Guid.NewGuid(), Positive = true };
        var newVote = new Vote
        {
            Id = Guid.NewGuid(),
            IdeaId = voteRequestDTO.IdeaId,
            UserId = Guid.NewGuid(),
            Positive = voteRequestDTO.Positive
        };
        _validatorMock.Setup(v => v.ValidateEntityToSave(voteRequestDTO)).Returns(newVote);
        _ideaServiceMock
            .Setup(s => s.GetByCriteria("id", newVote.IdeaId.ToString()))
            .Returns(
                new Idea()
                {
                    Id = Guid.NewGuid(),
                    Title = "Test Idea",
                    Description = "This is a test idea",
                    Date = DateTime.Now,
                    UserId = Guid.NewGuid(),
                    TopicId = Guid.NewGuid()
                }
            );
        _voteRepositoryMock.Setup(r => r.Save(newVote)).Returns(newVote);

        var result = _voteService.Save(voteRequestDTO);

        Assert.That(result, Is.SameAs(newVote));
    }

    [Test]
    public void Update_ReturnsUpdatedVote()
    {
        var voteId = Guid.NewGuid().ToString();
        var existingVote = new Vote
        {
            Id = Guid.Parse(voteId),
            IdeaId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Positive = true
        };
        var updateRequestDTO = new UpdateVoteRequestDTO { Positive = false };
        var updatedVote = new Vote
        {
            Id = existingVote.Id,
            IdeaId = existingVote.IdeaId,
            UserId = existingVote.UserId,
            Positive = updateRequestDTO.Positive
        };

        _voteRepositoryMock
            .Setup(r => r.GetByCriteria(It.IsAny<Func<Vote, bool>>()))
            .Returns(existingVote);
        _validatorMock
            .Setup(v => v.ValidateEntityToUpdate(existingVote, updateRequestDTO))
            .Returns(updatedVote);
        _voteRepositoryMock.Setup(r => r.Update(updatedVote)).Returns(updatedVote);

        var result = _voteService.Update(updateRequestDTO, voteId);

        Assert.That(result, Is.SameAs(updatedVote));
    }
}
