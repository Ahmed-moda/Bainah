using Bainah.Core.Entities;
using Bainah.Core.DTOs;
using Bainah.Core.Interfaces;
using Bainah.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Core.Entities;

namespace Bainah.Infrastructure.Repositories;

public class RolePermissionRepository : IRolePermissionRepository
{
    private readonly IdentityContext _db;

    public RolePermissionRepository(IdentityContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<int>> GetPermissionsByRoleAsync(Guid roleId)
    {
        return await _db.RolePermissions
            .Where(rp => rp.RoleId == roleId)
            .Select(rp => rp.PermissionId)
            .ToListAsync();
    }

    public async Task AddOrUpdatePermissionsAsync(RolePermissionsDto dto)
    {
        var existingPermissions = await _db.RolePermissions
            .Where(rp => rp.RoleId == dto.RoleId)
            .ToListAsync();

        // Remove permissions not in the new list
        var toRemove = existingPermissions
            .Where(rp => !dto.Permissions.Contains(rp.PermissionId))
            .ToList();

        if (toRemove.Any())
        {
            _db.RolePermissions.RemoveRange(toRemove);
        }

        // Add new permissions not already present
        var existingPermissionIds = existingPermissions.Select(rp => rp.PermissionId).ToHashSet();
        var toAdd = dto.Permissions
            .Where(p => !existingPermissionIds.Contains(p))
            .Select(p => new RolePermission { RoleId = dto.RoleId, PermissionId = p})
            .ToList();

        if (toAdd.Any())
        {
            await _db.RolePermissions.AddRangeAsync(toAdd);
        }

        await _db.SaveChangesAsync();
    }
}