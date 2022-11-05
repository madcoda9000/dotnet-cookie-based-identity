using Microsoft.AspNetCore.Authorization;

namespace DotNetIdentity.IdentitySettings.Requirements
{
    /// <summary>
    /// class to define own Identity requirements type of IAuthorizationRequirement
    /// </summary>
    public class FreeTrialExpireRequirement : IAuthorizationRequirement { }

    /// <summary>
    /// class to define own Identity requirements type of FreeTrialExpireRequirement
    /// </summary>
    public class FreeTrialExpireHandler : AuthorizationHandler<FreeTrialExpireRequirement>
    {
        /// <summary>
        /// overriding HandleRequirementAsync
        /// </summary>
        /// <param name="context">AuthorizationHandlerContext</param>
        /// <param name="requirement">FreeTrialExpireRequirement</param>
        /// <returns>Task</returns>
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