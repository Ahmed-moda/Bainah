
using Core.Entities;
using Microsoft.AspNetCore.Identity;
namespace Bainah.Core.Entities;
public class User : IdentityUser<Guid> {

    public string NationalId { get; set; }
    public bool Gender { get; set; }

    public string? OtpCode { get; set; }
    public DateTime? OtpExpiry { get; set; }
    public int OtpFailedAttempts { get; set; }
    public DateTime? LockoutEnd { get; set; }

    public int? CountryId { get; set; }
    public Country? Country { get; set; }

    public int? NationalityId { get; set; }
    public Nationality? Nationality { get; set; }

    public int? CityId { get; set; }
    public City? City { get; set; }

    public int? RegionId { get; set; }
    public Region? Region { get; set; }
}
public class Role : IdentityRole<Guid> {
    public ICollection<RolePermission> RolePermissions { get; set; }

}
