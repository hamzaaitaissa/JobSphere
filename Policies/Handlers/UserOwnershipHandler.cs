using JobSphere.Entities;
using JobSphere.Policies.Requirements;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;
using System.Security.Claims;

namespace JobSphere.Policies.Handlers
{
    public class UserOwnershipHandler : AuthorizationHandler<UserOwnershipRequirement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserOwnershipHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UserOwnershipRequirement requirement)
        {
            //System.Diagnostics.Debug.WriteLine("Hello");
            //var loggedInUserId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            //System.Diagnostics.Debug.WriteLine(loggedInUserId);
            //if (string.IsNullOrEmpty(loggedInUserId))
            //{
            //    context.Fail();
            //    return Task.CompletedTask;
            //}

            //// Get the user ID that the action is trying to edit (from the URL)
            //var userIdBeingEdited = _httpContextAccessor.HttpContext?.GetRouteData()?.Values;
            //System.Diagnostics.Debug.WriteLine("id "+userIdBeingEdited);
            //if (userIdBeingEdited == null)
            //{
            //    context.Fail();
            //    return Task.CompletedTask;
            //}

            //// Check if the logged-in user's ID matches the ID being edited
            //if (loggedInUserId == userIdBeingEdited.ToString())
            //{
            //    context.Succeed(requirement);  // If they match, allow the edit
            //}
            //else
            //{
            //    context.Fail();  // If they don't match, deny access
            //}

            //return Task.CompletedTask;
            var userIdClaim = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userRoleClaim = context.User.FindFirst(ClaimTypes.Role)?.Value;

            Debug.WriteLine($"Logged-in User ID Claim: {userIdClaim}");
            Debug.WriteLine($"Logged-in User Role Claim: {userRoleClaim}");

            if (userRoleClaim == "Admin")
            {
                Debug.WriteLine("User is an Admin, allowing access.");
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            var routeValues = _httpContextAccessor.HttpContext?.GetRouteData()?.Values;
            if (routeValues != null && routeValues.TryGetValue("id", out var idValue) && int.TryParse(idValue.ToString(), out var resourceId))
            {
                Debug.WriteLine($"ID from Route: {resourceId}");

                if (resourceId.ToString() == userIdClaim)
                {
                    Debug.WriteLine("User ID from route matches logged-in User ID, allowing access.");
                    context.Succeed(requirement);
                }
                else
                {
                    Debug.WriteLine($"User ID from route ({resourceId}) does NOT match logged-in User ID ({userIdClaim}).");
                }
            }
            else
            {
                Debug.WriteLine("Could not find 'id' route parameter or it's not an integer.");
            }

            return Task.CompletedTask;
        }
    }
}
