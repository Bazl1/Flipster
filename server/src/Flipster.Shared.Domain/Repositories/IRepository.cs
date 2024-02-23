namespace Flipster.Shared.Domain.Repositories;

public interface IRepository<T>
    where T : class
{
    void Add(T entity);
    void Remove(T entity);
    void Update(T entity);
    T? GetById(string id);
    IEnumerable<T> GetAll();
}
