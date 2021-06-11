using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ParishManager.Data;
using ParishManager.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParishManager.Attributes
{
    public class UserDetailsAttribute : ActionFilterAttribute
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public UserDetailsAttribute(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async override void OnActionExecuting(ActionExecutingContext filterContext) {
            var controller = filterContext.Controller as Controller;
            var username = filterContext.HttpContext.User.Identity.Name;

            if (controller != null && username != null)
            {

                var user = await _userManager.FindByEmailAsync(username);

                controller.ViewBag.FullName = $"{user.FirstName} {user.LastName}";
            }
        }
    }
}
