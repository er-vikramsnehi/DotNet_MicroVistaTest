using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroVistaMVC.Controllers
{
    
    public class FiltersController : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //HttpContext.Session.GetString("role")
            string currentUserRole = Convert.ToString(context.HttpContext.Session.GetString("UserRole"));

            if (!string.IsNullOrEmpty(currentUserRole))
            {
                if (currentUserRole != "Admin")
                {
                    context.Result = new RedirectToRouteResult
                (
                new RouteValueDictionary(new
                {
                    action = "Index",
                    controller = "Home"
                }));

                }
               // else
               // {
               //     context.Result = new RedirectToRouteResult
               //(
               //new RouteValueDictionary(new
               //{
               //    action = "Index",
               //    controller = "Admin"
               //}));

               // }
            }
            else
            {
                context.Result = new RedirectToRouteResult
                (
                new RouteValueDictionary(new
                {
                    action = "Index",
                    controller = "Home"
                }));

            }
        }
    }
}
