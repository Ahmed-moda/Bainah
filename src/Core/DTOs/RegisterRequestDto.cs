namespace Bainah.CoreApi.DTOs;
public class RegisterRequestDto {


    public string FullName { get; set; }
    public string NationalId { get; set; }
    public string PhoneNumber { get; set; } 
    public string Email { get; set; }
    public int Region { get; set; }
    public int City { get; set; }
    public int Country { get; set; }
    public int Nationality { get; set; }
    public bool Gender { get; set; }
}
