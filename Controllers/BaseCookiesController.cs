using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class BaseController : Controller
{
    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        if (Request.Cookies["Welcome"] == null)
        {
            Response.Cookies.Append("Welcome", "true", new CookieOptions { Expires = DateTime.Now.AddMonths(1) });
            ViewBag.Message = "Hey, Welcome to the Course Manager App!";
        }
        else
        {
            // Retrieve and display the last visited time
            if (Request.Cookies["LastVisited"] != null)
            {
                ViewBag.Message = $"Welcome back! You first used this app on: {Request.Cookies["LastVisited"]}";
            } else
            {
                string currentTime = DateTime.Now.ToString("G");
                Response.Cookies.Append("LastVisited", currentTime, new CookieOptions { Expires = DateTime.Now.AddMonths(1) });
                ViewBag.Message = $"Welcome back! You first used this app on: {currentTime}";
            }
        }

        base.OnActionExecuting(filterContext);
    }
}
