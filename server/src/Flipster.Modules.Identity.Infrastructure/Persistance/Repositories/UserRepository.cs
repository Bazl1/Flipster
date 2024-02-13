using Flipster.Modules.Identity.Domain.User.Repositories;

namespace Flipster.Modules.Identity.Infrastructure.Persistance.Repositories;

public class UserRepository(
    IdentityDbContext _db) : IUserRepository
{
    public void Create(Domain.User.Entities.User user)
    {
        _db.Add(user);
        _db.SaveChanges();
    }

    public void Update(Domain.User.Entities.User user)
    {
        _db.SaveChanges();
    }

    public void Remove(Domain.User.Entities.User user)
    {
        _db.Remove(user);
        _db.SaveChanges();
    }

    public Domain.User.Entities.User? FindByEmail(string email)
    {
        return _db.Users.SingleOrDefault(u => u.Email == email);
    }

    public Domain.User.Entities.User? FindByPhoneNumber(string phoneNumber)
    {
        return _db.Users.SingleOrDefault(u => u.PhoneNumber == phoneNumber);
    }

    public Domain.User.Entities.User? FindById(string id)
    {
        return _db.Users.SingleOrDefault(u => u.Id == id);
    }
}