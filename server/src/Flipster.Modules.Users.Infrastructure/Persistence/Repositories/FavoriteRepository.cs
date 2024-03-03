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
            .ToList();
    }

    public Favorite? GetByUserIdAndAdvertId(string userId, string advertId)
    {
        return _db.Favorites
            .Include(f => f.User)
            .SingleOrDefault(f => f.UserId == userId && f.AdvertId == advertId);
    }

    public Favorite? GetById(string userId, string advertId)
    {
        return _db.Favorites
            .SingleOrDefault(f => f.UserId == userId && f.AdvertId == advertId);
    }

    public void Update(string visitorId, string userId)
    {
        foreach (var favorite in _db.Favorites.Where(f => f.UserId == visitorId))
        {
            if (!_db.Favorites.Any(f => f.AdvertId == favorite.AdvertId && f.UserId == userId))
                _db.Add(new Favorite { AdvertId = favorite.AdvertId, UserId = userId });
            _db.Remove(favorite);
        }
        _db.SaveChanges();
    }
}