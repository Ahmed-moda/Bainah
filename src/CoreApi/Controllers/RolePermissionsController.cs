using Microsoft.AspNetCore.Mvc;
using Bainah.Core.Interfaces;
using Bainah.Core.DTOs;

namespace Bainah.CoreApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RolePermissionsController : ControllerBase
{
    private readonly IRolePermissionRepository _rolePermissionRepository;

    public RolePermissionsController(IRolePermissionRepository rolePermissionRepository)
    {
        _rolePermissionRepository = rolePermissionRepository;
    }

    // GET: api/RolePermissions/{roleId}
    [HttpGet("{roleId}")]
    public async Task<ActionResult<IEnumerable<int>>> GetPermissionsByRole(Guid roleId)
    {
        var permissions = await _rolePermissionRepository.GetPermissionsByRoleAsync(roleId);
        return Ok(permissions);
    }

    // POST: api/RolePermissions
    [HttpPost]
    public async Task<IActionResult> AddOrUpdatePermissions([FromBody] RolePermissionsDto dto)
    {
        await _rolePermissionRepository.AddOrUpdatePermissionsAsync(dto);
        return Ok();
    }
}