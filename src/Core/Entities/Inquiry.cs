
namespace Bainah.Core.Entities;
public class Inquiry
{
    public int Id { get; set; }
    public string Question { get; set; } = string.Empty;
    public string? Answer { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public int InquiryCategoryId { get; set; }
    public InquiryCategory? Category { get; set; }
    public int? AccountId { get; set; }
}
