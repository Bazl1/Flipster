namespace Flipster.Modules.Identity.Domain.User.Repositories;

public interface IUserRepository
{
    void Create(Entities.User user);
    void Update(Entities.User user);
    void Remove(Entities.User user);
    Entities.User? FindByEmail(string email);
    Entities.User? FindByPhoneNumber(string phoneNumber);
    Entities.User? FindById(string id);
}