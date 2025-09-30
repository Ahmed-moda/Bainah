using Bainah.Core.DTOs;

namespace Bainah.Core.Interfaces;

public interface IRolePermissionRepository
{
    Task<IEnumerable<int>> GetPermissionsByRoleAsync(Guid roleId);
    Task AddOrUpdatePermissionsAsync(RolePermissionsDto dto);
}