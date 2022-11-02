using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace DotNetIdentity.IdentitySettings.Requirements
{
    public class MinimumAgeRequirement : IAuthorizationRequirement
    {
        public MinimumAgeRequirement(int minimumAge)
        {
            MinimumAge = minimumAge;
        }

        public int MinimumAge { get; set; }
    }

    public class MinimumAgeHandler : AuthorizationHandler<MinimumAgeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
        {
            var birthDayClaim = context.User.FindFirst(f => f.Type == ClaimTypes.DateOfBirth);
            if (birthDayClaim != null)
            {
                var birthDay = Convert.ToDateTime(birthDayClaim.Value);
                var calculatedAge = DateTime.Now.Year - birthDay.Year;
                if (calculatedAge >= requirement.MinimumAge)
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