using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Bainah.Core.Entities;
using Core.Entities;
namespace Bainah.Infrastructure.Persistence;
public class IdentityContext : IdentityDbContext<User, Role, Guid>
{
    public IdentityContext(DbContextOptions<IdentityContext> options) : base(options) { }


    public DbSet<Permission> Permissions { get; set; }
    public DbSet<RolePermission> RolePermissions { get; set; }



}
