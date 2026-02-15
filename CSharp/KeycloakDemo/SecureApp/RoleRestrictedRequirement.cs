using Microsoft.AspNetCore.Authorization;

namespace SecureApp;

public class RoleRestrictedRequirement :
  AuthorizationHandler<RoleRestrictedRequirement>,
  IAuthorizationRequirement
{
  protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleRestrictedRequirement requirement)
  {
    // Check if the user is authenticated
    if (context.User?.Identity?.IsAuthenticated == true)
    {
      // User is authenticated, allow access
      context.Succeed(requirement);
    }
    // If not authenticated, do not call Succeed - the requirement will fail
  }
}