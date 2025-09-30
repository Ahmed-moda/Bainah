namespace Bainah.Core.Entities;

public class City
{
    public int Id { get; set; }
    public string NameAr { get; set; } = default!;
    public string NameEn { get; set; } = default!;
    public int RegionId { get; set; }
    public Region? Region { get; set; }
}