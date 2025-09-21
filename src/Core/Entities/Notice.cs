
namespace Bainah.Core.Entities;
public class Notice
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime PublishedAt { get; set; } = DateTime.UtcNow;
    public int NoticeCategoryId { get; set; }
    public NoticeCategory? Category { get; set; }
}
