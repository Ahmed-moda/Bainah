
namespace Bainah.Core.Entities;
public class WebPage
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public ICollection<PageSection> Sections { get; set; } = new List<PageSection>();
}
