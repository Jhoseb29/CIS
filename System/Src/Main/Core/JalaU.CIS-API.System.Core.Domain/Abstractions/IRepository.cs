namespace JalaU.CIS_API.System.Core.Domain;

public interface IRepository<T>
{
    IEnumerable<T> GetAll();
    T GetById(Guid id);
    T Save(T entity);
    T Delete(T entity);
}
