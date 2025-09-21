using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Bainah.Core.Entities;
namespace Bainah.Infrastructure.Persistence;
public class IdentityContext : IdentityDbContext<User, Role, int>
{
    public IdentityContext(DbContextOptions<IdentityContext> options) : base(options) { }
}
