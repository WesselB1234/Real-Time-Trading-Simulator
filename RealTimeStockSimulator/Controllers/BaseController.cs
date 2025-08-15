using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RealTimeStockSimulator.Extensions;
using RealTimeStockSimulator.Models;

namespace RealTimeStockSimulator.Controllers
{
    public class BaseController : Controller
    {
        protected User? LoggedInUser { get; private set; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            LoggedInUser = HttpContext.Session.GetObject<User>("LoggedInUser");

            base.OnActionExecuting(context);
        }
    }
}
