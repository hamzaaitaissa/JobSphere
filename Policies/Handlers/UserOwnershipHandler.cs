using JobSphere.Entities;
using JobSphere.Policies.Requirements;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace JobSphere.Policies.Handlers
{
    public class UserOwnershipHandler : AuthorizationHandler<UserOwnershipRequirement, User>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UserOwnershipRequirement requirement, User user)
        {
            var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userRole = context.User.FindFirst(ClaimTypes.Role)?.Value;
            if (user.Id.ToString() == userId || userRole == "Admin")
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
