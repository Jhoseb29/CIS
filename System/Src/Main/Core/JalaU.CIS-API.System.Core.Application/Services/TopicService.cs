using JalaU.CIS_API.System.Core.Domain;

namespace JalaU.CIS_API.System.Core.Application;

public class TopicService(IRepository<Topic> topicRepository) : IService<Topic>
{
    private readonly IRepository<Topic> _topicRepository = topicRepository;

    public List<Topic> GetAll()
    {
        throw new NotImplementedException();
    }

    public Topic GetById(Guid guid)
    {
        throw new NotImplementedException();
    }

    public Topic Save(BaseRequestDTO entityToSave)
    {
        throw new NotImplementedException();
    }

    public Topic Update(BaseRequestDTO entityToSave, string id)
    {
        throw new NotImplementedException();
    }

    public Topic DeleteById(Guid guid)
    {
        throw new NotImplementedException();
    }
}
