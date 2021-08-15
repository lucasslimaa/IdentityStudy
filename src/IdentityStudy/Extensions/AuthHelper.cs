using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityStudy.Extensions
{
    public class RequiredPermissions : IAuthorizationRequirement
    {
        public string Permission {get;}

        public RequiredPermissions(string permission)
        {
            Permission = permission;
        }
    }

    public class RequiredPermissionsHandler : AuthorizationHandler<RequiredPermissions>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RequiredPermissions requirement)
        {
            if (context.User.HasClaim(c => c.Type == "Permission" && c.Value.Contains(requirement.Permission)))
            {
                context.Succeed(requirement);
            };

            return Task.CompletedTask;
        }
    }
}
