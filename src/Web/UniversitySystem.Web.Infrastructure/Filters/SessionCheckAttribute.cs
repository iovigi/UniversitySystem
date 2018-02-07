namespace UniversitySystem.Web.Infrastructure.Filters
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.AspNetCore.Http;


    public class SessionCheckAttribute : ActionFilterAttribute
    {
        private const string userIdSessionVariable = "userId";

        private readonly string controllerName;
        private readonly string actionName;

        public SessionCheckAttribute(string controllerName, string actionName)
        {
            this.controllerName = controllerName;
            this.actionName = actionName;
        }
        /// <summary>
        /// On executing checking if user, has session, by trying get user Id 
        /// </summary>
        /// <param name="filterContext">filterContext</param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            if (filterContext.HttpContext.Session.Get(userIdSessionVariable) == null)
            {
                filterContext.Result = new RedirectToActionResult(this.actionName, this.controllerName, new { });

                return;
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
