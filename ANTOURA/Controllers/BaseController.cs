using ANTOURA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ANTOURA.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        private AccountRepository accountRpstry = new AccountRepository();
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userEntry = (new UsersContext()).UserProfiles.FirstOrDefault(d=>d.UserName == User.Identity.Name);
                ViewBag.LoggedUser = accountRpstry.GetUserMembership(userEntry.UserId);
            }
            base.OnActionExecuting(filterContext);
        }
       
    }

}
