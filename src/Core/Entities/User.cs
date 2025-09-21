
using Microsoft.AspNetCore.Identity;
namespace Bainah.Core.Entities;
public class User : IdentityUser<int> {

    public string NationalId { get; set; }
    public string Region { get; set; }
    public string City { get; set; }
    public string? Street { get; set; }

    public string? OtpCode { get; set; }
    public DateTime? OtpExpiry { get; set; }
    public int OtpFailedAttempts { get; set; }
    public DateTime? LockoutEnd { get; set; }

}
public class Role : IdentityRole<int> { }
