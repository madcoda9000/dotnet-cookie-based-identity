using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace DotNetIdentity.IdentitySettings.Requirements
{
    /// <summary>
    /// class to define own Identity requirements type of IAuthorizationRequirement
    /// </summary>
    public class MinimumAgeRequirement : IAuthorizationRequirement
    {
        /// <summary>
        /// class constructor
        /// </summary>
        /// <param name="minimumAge"></param>
        public MinimumAgeRequirement(int minimumAge)
        {
            MinimumAge = minimumAge;
        }

        /// <summary>
        /// property MinimumAge
        /// </summary>
        /// <value></value>
        public int MinimumAge { get; set; }
    }

    /// <summary>
    /// class implementing AuthorizationHandler of type MinimumAgeRequirement
    /// </summary>
    public class MinimumAgeHandler : AuthorizationHandler<MinimumAgeRequirement>
    {
        /// <summary>
        /// overriding HandleRequirementAsync
        /// </summary>
        /// <param name="context">AuthorizationHandlerContext</param>
        /// <param name="requirement">MinimumAgeRequirement</param>
        /// <returns>Task</returns>
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