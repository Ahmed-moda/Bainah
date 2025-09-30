namespace Bainah.Core.DTOs;

public class RolePermissionsDto
{
    public Guid RoleId { get; set; }
    public List<int> Permissions { get; set; }
}