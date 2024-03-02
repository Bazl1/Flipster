using Flipster.Modules.Chats.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Flipster.Modules.Chats.Infrastructure.Persistence;

public class ChatsModuleDbContext : DbContext
{
    public DbSet<Chat> Chats { get; set; }
    public DbSet<Message> Messages { get; set; }

    public ChatsModuleDbContext(DbContextOptions<ChatsModuleDbContext> options) : base(options)
    {
    }
}
