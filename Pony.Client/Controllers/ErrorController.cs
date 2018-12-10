namespace Pony.Client.Controllers
{
    using System.Web.Mvc;

    public class ErrorController : Controller
    {
        /// <summary>
        /// 404 error
        /// </summary>
        /// <returns>Return View NotFound</returns>
        public ActionResult NotFound()
        {
            return this.View();
        }

        /// <summary>
        /// Other error
        /// </summary>
        /// <returns>Return View Other</returns>
        public ActionResult Other()
        {
            return this.View();
        }
    }
}