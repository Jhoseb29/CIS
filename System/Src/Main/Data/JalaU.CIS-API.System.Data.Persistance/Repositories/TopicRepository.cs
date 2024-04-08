using JalaU.CIS_API.System.Core.Domain;

namespace JalaU.CIS_API.System.Data.Persistance;

public class TopicRepository(AppDbContext appDbContext) : IRepository<Topic>
{
    private readonly AppDbContext _appDbContext = appDbContext;

    public IEnumerable<Topic> GetAll()
    {
        throw new NotImplementedException();
    }

    public Topic GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Topic Save(Topic entity)
    {
        _appDbContext.Add(entity);
        _appDbContext.SaveChanges();
        return entity;
    }

    public Topic Delete(Topic entity)
    {
        throw new NotImplementedException();
    }
}
