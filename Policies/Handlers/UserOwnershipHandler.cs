using JobSphere.Entities;
using JobSphere.Policies.Requirements;
using JobSphere.Repositories.Jobs;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;
using System.Security.Claims;

namespace JobSphere.Policies.Handlers
{
    public class UserOwnershipHandler : AuthorizationHandler<UserOwnershipRequirement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IJobRepository _jobRepository;

        public UserOwnershipHandler(IHttpContextAccessor httpContextAccessor, IJobRepository jobRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _jobRepository = jobRepository;
        }
        protected async override Task HandleRequirementAsync(AuthorizationHandlerContext context, UserOwnershipRequirement requirement)
        {

            var userIdClaim = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userRoleClaim = context.User.FindFirst(ClaimTypes.Role)?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
            {
                Debug.WriteLine("User ID claim is missing.");
                return;
            }

            Debug.WriteLine($"Logged-in User ID Claim: {userIdClaim}");
            Debug.WriteLine($"Logged-in User Role Claim: {userRoleClaim}");

            if (userRoleClaim == "Admin")
            {
                Debug.WriteLine("User is an Admin, allowing access.");
                context.Succeed(requirement);
                return ;
            }

            var httpContext = _httpContextAccessor.HttpContext;
            var routeValues = httpContext?.GetRouteData()?.Values;

            if (routeValues == null || !routeValues.TryGetValue("id", out var idValue) || !int.TryParse(idValue.ToString(), out var resourceId))
            {
                Debug.WriteLine("Invalid or missing route ID.");
                return ;
            }

            var path = httpContext?.Request.Path.Value?.ToLower();

            if (path != null && path.Contains("/jobs"))
            {
                var job = await _jobRepository.GetByIdAsync(resourceId);
                if (job != null && job.EmployerId.ToString() == userIdClaim)
                {
                    Debug.WriteLine("Job belongs to the logged-in user. Access granted.");
                    context.Succeed(requirement);
                }
                else
                {
                    Debug.WriteLine("Job does not belong to the user.");
                }
            }
            else if (resourceId.ToString() == userIdClaim)
            {
                Debug.WriteLine("User ID from route matches logged-in User ID, access granted.");
                context.Succeed(requirement);
            }
            else
            {
                Debug.WriteLine("User ID mismatch.");
            }
        }
        }
}
