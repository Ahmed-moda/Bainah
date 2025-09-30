using Bainah.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Security.CustomAuthoraize
{
    public class PermissionAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private readonly string _role;
        private readonly string _permission;

        public PermissionAuthorizeAttribute(string role, string permission)
        {
            _role = role;
            _permission = permission;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            if (!user.Identity?.IsAuthenticated ?? true)
            {
                context.Result = new ForbidResult();
                return;
            }

            var db = context.HttpContext.RequestServices.GetRequiredService<IdentityContext>();
            var userManager = context.HttpContext.RequestServices.GetRequiredService<UserManager<IdentityUser>>();

            var userId = userManager.GetUserId(user);
            var userEntity = userManager.FindByIdAsync(userId).Result;

            // ✅ check if user is in the given role
            if (!userManager.IsInRoleAsync(userEntity, _role).Result)
            {
                context.Result = new ForbidResult();
                return;
            }

            // ✅ check if that role has the required permission
            var allowed = db.RolePermissions
                .Include(rp => rp.Permission)
                .Any(rp => rp.Role.Name == _role && rp.Permission.Name == _permission);

            if (!allowed)
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
