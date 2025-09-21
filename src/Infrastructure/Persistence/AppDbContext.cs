using Microsoft.EntityFrameworkCore;
using Bainah.Core.Entities;
using Core.Entities;
namespace Bainah.Infrastructure.Persistence;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> opts) : base(opts) { }
    public DbSet<Notice> Notices { get; set; } = null!;
    public DbSet<NoticeCategory> NoticeCategories { get; set; } = null!;
    public DbSet<Inquiry> Inquiries { get; set; } = null!;
    public DbSet<InquiryCategory> InquiryCategories { get; set; } = null!;
    public DbSet<EventCategory> EventCategories { get; set; } = null!;
    public DbSet<Account> Accounts { get; set; } = null!;
    public DbSet<WebPage> WebPages { get; set; } = null!;
    public DbSet<PageSection> PageSections { get; set; } = null!;
    public DbSet<PageSectionPhoto> PageSectionPhoto { get; set; } = null!;
}
