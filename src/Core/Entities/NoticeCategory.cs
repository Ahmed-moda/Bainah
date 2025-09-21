
namespace Bainah.Core.Entities;
public class NoticeCategory
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<Notice> Notices { get; set; } = new List<Notice>();
}
