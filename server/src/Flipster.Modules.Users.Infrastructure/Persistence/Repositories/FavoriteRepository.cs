using Flipster.Modules.Users.Domain.Entities;
using Flipster.Modules.Users.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Flipster.Modules.Users.Infrastructure.Persistence.Repositories;

public class FavoriteRepository(
    UsersModuleDbContext _db) : IFavoriteRepository
{
    public void Add(Favorite entity)
    {
        _db.Favorites.Add(entity);
        _db.SaveChanges();
    }
    
    public void Remove(Favorite entity)
    {
        _db.Favorites.Remove(entity);
        _db.SaveChanges();
    }

    public void Update(Favorite entity)
    {
        _db.Entry(entity).State = EntityState.Modified;
        _db.SaveChanges();
    }

    public Favorite? GetById(string id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Favorite> GetAll()
    {
        return _db.Favorites
            .Include(f => f.User)
            .ToList();
    }

    public IEnumerable<Favorite> GetByUserId(string userId)
    {
        return _db.Favorites
            .Where(f => f.UserId == userId)
            .Include(f => f.User)
            .ToList();
    }

    public Favorite? GetByUserIdAndAdvertId(string userId, string advertId)
    {
        return _db.Favorites
            .Include(f => f.User)
            .SingleOrDefault(f => f.UserId == userId && f.AdvertId == advertId);
    }
}