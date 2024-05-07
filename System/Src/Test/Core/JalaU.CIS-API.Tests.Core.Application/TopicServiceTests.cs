/*using JalaU.CIS_API.System.Core.Application;
using JalaU.CIS_API.System.Core.Domain;
using Moq;

namespace JalaU.CIS_API.Tests.Core.Application;

[TestFixture]
public class TopicServiceTests
{
    private Mock<IRepository<Topic>> _topicRepositoryMock;
    private Mock<AbstractValidator<Topic>> _validatorMock;
    private Mock<EntityFilter<Topic>> _entityFilterMock;
    private TopicService _topicService;

    [SetUp]
    public void Setup()
    {
        _topicRepositoryMock = new Mock<IRepository<Topic>>();
        _validatorMock = new Mock<AbstractValidator<Topic>>();
        _entityFilterMock = new Mock<EntityFilter<Topic>>();

        _topicService = new TopicService(
            _topicRepositoryMock.Object,
            _validatorMock.Object,
            _entityFilterMock.Object
        );
    }

    [Test]
    public void GetAll_ReturnsListOfTopicsWithFiltering()
    {
        var topics = new List<Topic>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Title = "Topic 1",
                Description = "Test Topic",
                Date = DateTime.Now,
                Labels = ["test"],
                UserId = Guid.NewGuid()
            },
            new()
            {
                Id = Guid.NewGuid(),
                Title = "Topic 2",
                Description = "Test Topic",
                Date = DateTime.Now,
                Labels = ["test"],
                UserId = Guid.NewGuid()
            },
            new()
            {
                Id = Guid.NewGuid(),
                Title = "Topic 3",
                Description = "Test Topic",
                Date = DateTime.Now,
                Labels = ["test"],
                UserId = Guid.NewGuid()
            }
        };
        var getAllEntitiesDTO = new GetAllEntitiesRequestDTO
        {
            PageSize = 2,
            PageNumber = 1,
            OrderBy = "title",
            Order = "asc",
            Filter = "",
            Keyword = ""
        };
        var filteredTopics = topics.GetRange(0, getAllEntitiesDTO.PageSize);
        _topicRepositoryMock.Setup(r => r.GetAll()).Returns(topics.AsQueryable());
        _entityFilterMock
            .Setup(f => f.Filter(topics, getAllEntitiesDTO.Filter, getAllEntitiesDTO.Keyword))
            .Returns(filteredTopics);

        var result = _topicService.GetAll(getAllEntitiesDTO);

        CollectionAssert.AreEqual(filteredTopics, result);
    }

    [Test]
    public void GetByCriteria_ReturnsTopicById()
    {
        var topicId = Guid.NewGuid().ToString();
        var topic = new Topic
        {
            Id = Guid.Parse(topicId),
            Title = "Test Topic",
            Description = "Test Topic",
            Date = DateTime.Now,
            Labels = ["test"],
            UserId = Guid.NewGuid()
        };
        _topicRepositoryMock
            .Setup(r => r.GetByCriteria(It.IsAny<Func<Topic, bool>>()))
            .Returns(topic);

        var result = _topicService.GetByCriteria("id", topicId);

        Assert.That(result, Is.SameAs(topic));
    }

    [Test]
    public void GetByCriteria_ReturnsTopicByTitle()
    {
        var title = "Test Topic";
        var topic = new Topic
        {
            Id = Guid.NewGuid(),
            Title = title,
            Description = "Test Topic",
            Date = DateTime.Now,
            Labels = ["test"],
            UserId = Guid.NewGuid()
        };
        _topicRepositoryMock
            .Setup(r => r.GetByCriteria(It.IsAny<Func<Topic, bool>>()))
            .Returns(topic);

        var result = _topicService.GetByCriteria("title", title);

        Assert.That(result, Is.SameAs(topic));
    }

    [Test]
    public void GetByCriteria_ThrowsArgumentExceptionForInvalidField()
    {
        var invalidField = "invalidField";
        Assert.Throws<ArgumentException>(() => _topicService.GetByCriteria(invalidField, "value"));
    }

    [Test]
    public void Save_ReturnsNewTopic()
    {
        GlobalVariables.UserId = Guid.NewGuid().ToString();
        var topicRequestDTO = new TopicRequestDTO { Title = "New Topic" };
        var newTopic = new Topic
        {
            Id = Guid.NewGuid(),
            Title = topicRequestDTO.Title,
            Description = "Test Topic",
            Date = DateTime.Now,
            Labels = ["test"],
            UserId = Guid.NewGuid()
        };
        _validatorMock.Setup(v => v.ValidateEntityToSave(topicRequestDTO)).Returns(newTopic);
        _topicRepositoryMock.Setup(r => r.Save(newTopic)).Returns(newTopic);

        var result = _topicService.Save(topicRequestDTO);

        Assert.That(result, Is.SameAs(newTopic));
    }

    [Test]
    public void Update_ReturnsUpdatedTopic()
    {
        var topicId = Guid.NewGuid().ToString();
        var existingTopic = new Topic
        {
            Id = Guid.Parse(topicId),
            Title = "Existing Topic",
            Description = "Test Topic",
            Date = DateTime.Now,
            Labels = ["test"],
            UserId = Guid.NewGuid()
        };
        var updateRequestDTO = new TopicRequestDTO { Title = "Updated Topic" };
        var updatedTopic = new Topic
        {
            Id = existingTopic.Id,
            Title = updateRequestDTO.Title,
            Description = "Test Topic",
            Date = DateTime.Now,
            Labels = ["test"],
            UserId = existingTopic.UserId
        };

        _topicRepositoryMock
            .Setup(r => r.GetByCriteria(It.IsAny<Func<Topic, bool>>()))
            .Returns(existingTopic);
        _validatorMock
            .Setup(v => v.ValidateEntityToUpdate(existingTopic, updateRequestDTO))
            .Returns(updatedTopic);
        _topicRepositoryMock.Setup(r => r.Update(updatedTopic)).Returns(updatedTopic);

        var result = _topicService.Update(updateRequestDTO, topicId);

        Assert.That(result, Is.SameAs(updatedTopic));
    }

    [Test]
    public void DeleteById_ReturnsDeletedTopic()
    {
        var topicId = Guid.NewGuid().ToString();
        var deletedTopic = new Topic
        {
            Id = Guid.Parse(topicId),
            Title = "Test Topic",
            Description = "Test Topic",
            Date = DateTime.Now,
            Labels = ["test"],
            UserId = Guid.NewGuid()
        };
        _topicRepositoryMock
            .Setup(r => r.GetByCriteria(It.IsAny<Func<Topic, bool>>()))
            .Returns(deletedTopic);

        var result = _topicService.DeleteById(topicId);

        Assert.That(result, Is.SameAs(deletedTopic));
        _topicRepositoryMock.Verify(r => r.Delete(deletedTopic), Times.Once);
    }
}
*/
