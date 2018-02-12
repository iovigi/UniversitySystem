namespace UniversitySystem.Web.Infrastructure.Filters
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    using Extensions;

    public class CheckIfUserIsLoginAttribute : ActionFilterAttribute
    {
        private readonly string controllerName;
        private readonly string actionName;

        public CheckIfUserIsLoginAttribute(string controllerName, string actionName)
        {
            this.controllerName = controllerName;
            this.actionName = actionName;
        }
        /// <summary>
        /// On executing checking if user, is login, by trying get user Id and if user is login redirect to specific controller
        /// </summary>
        /// <param name="filterContext">filterContext</param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.IsUserLoged())
            {
                filterContext.Result = new RedirectToActionResult(this.actionName, this.controllerName, new { });

                return;
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
