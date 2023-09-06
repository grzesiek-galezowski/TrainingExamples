using Microsoft.AspNetCore.Authorization;

namespace SecureApp;

public class RoleRestrictedRequirement :
  AuthorizationHandler<RoleRestrictedRequirement>,
  IAuthorizationRequirement
{
  protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleRestrictedRequirement requirement)
  {
    context.Succeed(requirement);
  }
}