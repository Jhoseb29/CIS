namespace JalaU.CIS_API.System.Core.Domain;

public interface IService<T>
{
    List<T> GetAll();
    T GetById(Guid guid);
    T Save(BaseRequestDTO entityToSave);
    T Update(BaseRequestDTO entityToSave, string id);
    T DeleteById(Guid guid);
}
