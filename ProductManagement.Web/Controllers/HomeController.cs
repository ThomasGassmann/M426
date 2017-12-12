namespace ProductManagement.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Defines the controller for the route 'Home/Index'. This is the default route.
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Returns the default page.
        /// </summary>
        /// <returns>The default page.</returns>
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
