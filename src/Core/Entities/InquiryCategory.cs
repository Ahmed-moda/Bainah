
namespace Bainah.Core.Entities;
public class InquiryCategory
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<Inquiry> Inquiries { get; set; } = new List<Inquiry>();
}
