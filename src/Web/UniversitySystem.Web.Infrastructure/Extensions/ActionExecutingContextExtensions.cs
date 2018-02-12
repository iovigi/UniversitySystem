namespace UniversitySystem.Web.Infrastructure.Extensions
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Filters;

    public static class ActionExecutingContextExtensions
    {
        /// <summary>
        /// Check, if user in the session.
        /// </summary>
        /// <param name="context">ActionExecutingContext</param>
        /// <returns>Return true, if user is in session, otherwise return false</returns>
        public static bool IsUserLoged(this ActionExecutingContext context)
        {
            return context.HttpContext.User.Identity.IsAuthenticated;
        }
    }
}
