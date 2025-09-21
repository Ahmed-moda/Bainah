namespace Bainah.CoreApi.DTOs;
public class RegisterRequestDto {
    public string NationalId { get; set; }
    public string PhoneNumber { get; set; } 
    public string Email { get; set; }
    public string Region { get; set; }
    public string City { get; set; }
    public string? Street { get; set; }
}
