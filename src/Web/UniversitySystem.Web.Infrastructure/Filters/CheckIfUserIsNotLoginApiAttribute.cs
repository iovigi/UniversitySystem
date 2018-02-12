namespace UniversitySystem.Web.Infrastructure.Filters
{
    using System.IO;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    using Newtonsoft.Json;

    using Extensions;

    public class CheckIfUserIsNotLoginApiAttribute : ActionFilterAttribute
    {
        private readonly string controllerName;
        private readonly string actionName;

        public CheckIfUserIsNotLoginApiAttribute(string controllerName, string actionName)
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
            if (!filterContext.IsUserLoged())
            {
                var response = filterContext.HttpContext.Response;

                response.StatusCode = 403;
                response.ContentType = "application/json";

                using (var writer = new StreamWriter(response.Body))
                {
                    new JsonSerializer().Serialize(writer, new { redirectUrl = string.Format("/{0}/{1}", this.controllerName, this.actionName) });
                    writer.Flush();
                }

                filterContext.Result = new ForbidResult();

                return;
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
