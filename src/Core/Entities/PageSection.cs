
using Core.Entities;

namespace Bainah.Core.Entities;
public class PageSection
{
    public int Id { get; set; }
    public string Header { get; set; } = string.Empty;
    public string HeaderAr { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string ContentAr { get; set; } = string.Empty;
    public int Order { get; set; }

    public int WebPageId { get; set; }
    public WebPage WebPage { get; set; } = default!;

    // Navigation Property
    public ICollection<PageSectionPhoto> Photos { get; set; } = new List<PageSectionPhoto>();
}
