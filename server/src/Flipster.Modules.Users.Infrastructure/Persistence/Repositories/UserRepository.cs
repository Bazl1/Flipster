using Flipster.Modules.Users.Domain.Entities;
using Flipster.Modules.Users.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Flipster.Modules.Users.Infrastructure.Persistence.Repositories;

internal class UserRepository(
    UsersModuleDbContext _db) : IUserRepository
{
    public void Add(User entity)
    {
        _db.Users.Add(entity);
        _db.SaveChanges();
    }

    public IEnumerable<User> GetAll()
    {
        return _db.Users.ToList();
    }

    public User? GetByEmail(string email)
    {
        return _db.Users.SingleOrDefault(u => u.Email.ToLower() == email.ToLower());
    }

    public User? GetById(string id)
    {
        return _db.Users.SingleOrDefault(u => u.Id == id);
    }

    public IEnumerable<User> GetByLocation(string location)
    {
        return _db.Users
            .Include(u => u.Location)
            .Where(u => u.Location != null && u.Location.Value == location);
    }

    public IEnumerable<Query> GetByQueryHistoryById(string id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<View> GetViewsById(string id)
    {
        throw new NotImplementedException();
    }

    public void Remove(User entity)
    {
        _db.Remove(entity);
        _db.SaveChanges();
    }

    public void Update(User entity)
    {
        _db.SaveChanges();
    }
}
