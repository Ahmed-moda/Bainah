namespace Bainah.Core.Entities;

public class Region
{
    public int Id { get; set; }
    public string NameAr { get; set; } = default!;
    public string NameEn { get; set; } = default!;
    public ICollection<City>? Cities { get; set; }
}