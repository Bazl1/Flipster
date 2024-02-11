namespace Flipster.Identity.Core.Data.Seeds;

public interface IDataSeeder<T>
{
    void Seed(ApplicationDbContext context);
}