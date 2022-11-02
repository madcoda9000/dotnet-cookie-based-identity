using Microsoft.AspNetCore.Authorization;

namespace DotNetIdentity.IdentitySettings.Requirements
{
    public class FreeTrialExpireRequirement : IAuthorizationRequirement { }

    public class FreeTrialExpireHandler : AuthorizationHandler<FreeTrialExpireRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, FreeTrialExpireRequirement requirement)
        {
            var freeTrialClaim = context.User.FindFirst(f => f.Type == "FreeTrial");
            if (freeTrialClaim != null)
            {
                var freeTrialExpires = Convert.ToDateTime(freeTrialClaim.Value).AddDays(30);
                if (DateTime.Now < freeTrialExpires)
                {
                    context.Succeed(requirement);
                }
                else
                {
                    context.Fail();
                }
            }

            return Task.CompletedTask;
        }
    }
}