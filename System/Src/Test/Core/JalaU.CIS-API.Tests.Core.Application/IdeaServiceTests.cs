using JalaU.CIS_API.System.Core.Application;
using JalaU.CIS_API.System.Core.Domain;
using Moq;

namespace JalaU.CIS_API.Tests.Core.Application;

public class IdeaServiceTests
{
    private Mock<IRepository<Idea>> _voteRepositoryMock;
    private Mock<IService<Topic>> _topicServiceMock;
    private Mock<AbstractValidator<Idea>> _validatorMock;
    private Mock<EntityFilter<Idea>> _entityFilterMock;
    private IdeaService _ideaService;

    [SetUp]
    public void Setup()
    {
        _voteRepositoryMock = new Mock<IRepository<Idea>>();
        _topicServiceMock = new Mock<IService<Topic>>();
        _validatorMock = new Mock<AbstractValidator<Idea>>();
        _entityFilterMock = new Mock<EntityFilter<Idea>>();

        _ideaService = new IdeaService(
            _voteRepositoryMock.Object,
            _topicServiceMock.Object,
            _validatorMock.Object,
            _entityFilterMock.Object
        );
    }

    [Test]
    public void DeleteById_ReturnsDeletedIdea()
    {
        var ideaId = Guid.NewGuid().ToString();
        var deletedIdea = new Idea()
        {
            Id = Guid.NewGuid(),
            Title = "Test topic",
            Description = "Test Topic",
            Date = DateTime.Now,
            TopicId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
        };
        _voteRepositoryMock
            .Setup(repository => repository.GetByCriteria(It.IsAny<Func<Idea, bool>>()))
            .Returns(deletedIdea);
        var result = _ideaService.DeleteById(ideaId);

        Assert.That(result, Is.SameAs(deletedIdea));
        _voteRepositoryMock.Verify(r => r.Delete(deletedIdea), Times.Once);
    }

    [Test]
    public void GetAll_ReturnsListOfIdeasWithFiltering()
    {
        var ideas = new List<Idea>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Title = "3 Test topic",
                Description = "Test Topic",
                Date = DateTime.Now,
                TopicId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
            },
            new()
            {
                Id = Guid.NewGuid(),
                Title = "2 Test topic",
                Description = "Test Topic",
                Date = DateTime.Now,
                TopicId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
            },
            new()
            {
                Id = Guid.NewGuid(),
                Title = "1 Test topic",
                Description = "Test Topic",
                Date = DateTime.Now,
                TopicId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
            }
        };
        var getAllEntitiesDTO = new GetAllEntitiesRequestDTO
        {
            PageSize = 5,
            PageNumber = 1,
            OrderBy = "title",
            Order = "desc",
            Filter = "title",
            Keyword = "topic"
        };
        _voteRepositoryMock.Setup(r => r.GetAll()).Returns(ideas.AsQueryable());
        _entityFilterMock
            .Setup(f => f.Filter(ideas, getAllEntitiesDTO.Filter, getAllEntitiesDTO.Keyword))
            .Returns(ideas);

        var result = _ideaService.GetAll(getAllEntitiesDTO);

        CollectionAssert.AreEqual(ideas, result);
    }

    [Test]
    public void GetByCriteria_ReturnsIdeaById()
    {
        var ideaId = Guid.NewGuid().ToString();
        var idea = new Idea()
        {
            Id = Guid.NewGuid(),
            Title = "1 Test topic",
            Description = "Test Topic",
            Date = DateTime.Now,
            TopicId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
        };
        _voteRepositoryMock.Setup(r => r.GetByCriteria(It.IsAny<Func<Idea, bool>>())).Returns(idea);

        var result = _ideaService.GetByCriteria("id", ideaId);

        Assert.That(result, Is.SameAs(idea));
    }

    [Test]
    public void GetByCriteria_ThrowsArgumentExceptionForInvalidField()
    {
        var invalidField = "invalidField";
        Assert.Throws<ArgumentException>(() => _ideaService.GetByCriteria(invalidField, "value"));
    }

    [Test]
    public void Save_ReturnsNewIdea()
    {
        GlobalVariables.UserId = Guid.NewGuid().ToString();

        var ideaRequestDTO = new IdeaRequestDTO
        {
            Title = "1 Test topic",
            Description = "Test Topic",
            TopicId = Guid.NewGuid(),
        };
        var newIdea = new Idea
        {
            Id = Guid.NewGuid(),
            Title = "1 Test topic",
            Description = "Test Topic",
            Date = DateTime.Now,
            TopicId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
        };
        _validatorMock.Setup(idea => idea.ValidateEntityToSave(ideaRequestDTO)).Returns(newIdea);
        _topicServiceMock
            .Setup(s => s.GetByCriteria("id", newIdea.TopicId.ToString()))
            .Returns(
                new Topic()
                {
                    Id = Guid.NewGuid(),
                    Title = "Test Idea",
                    Description = "This is a test idea",
                    Labels = ["ddd", "sss"],
                    Date = DateTime.Now,
                    UserId = Guid.NewGuid(),
                }
            );
        _voteRepositoryMock.Setup(r => r.Save(newIdea)).Returns(newIdea);

        var result = _ideaService.Save(ideaRequestDTO);

        Assert.That(result, Is.SameAs(newIdea));
    }

    [Test]
    public void Update_ReturnsUpdatedIdea()
    {
        var voteId = Guid.NewGuid().ToString();
        var existingIdea = new Idea
        {
            Id = Guid.NewGuid(),
            Title = "1 Test topic",
            Description = "Test Topic",
            Date = DateTime.Now,
            TopicId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
        };
        var updateRequestDTO = new UpdateIdeaRequestDTO
        {
            Title = "1 Test topic2",
            Description = "Test Topi2c",
        };
        var updatedIdea = new Idea
        {
            Id = existingIdea.Id,
            Title = "1 Test topic",
            Description = "Test Topic",
            Date = existingIdea.Date,
            TopicId = existingIdea.TopicId,
            UserId = existingIdea.UserId,
        };

        _voteRepositoryMock
            .Setup(repository => repository.GetByCriteria(It.IsAny<Func<Idea, bool>>()))
            .Returns(existingIdea);
        _validatorMock
            .Setup(validator => validator.ValidateEntityToUpdate(existingIdea, updateRequestDTO))
            .Returns(updatedIdea);
        _voteRepositoryMock
            .Setup(repository => repository.Update(updatedIdea))
            .Returns(updatedIdea);

        var result = _ideaService.Update(updateRequestDTO, voteId);

        Assert.That(result, Is.SameAs(updatedIdea));
    }
}
