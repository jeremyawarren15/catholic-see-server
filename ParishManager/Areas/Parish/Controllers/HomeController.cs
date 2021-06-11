using Microsoft.AspNetCore.Mvc;
using ParishManager.Areas.Parish.Models.Home;
using ParishManager.Constants;
using ParishManager.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ParishManager.Areas.Parish.Controllers
{
    [Area(AreaName.Parish)]
    public class HomeController : Controller
    {
        private readonly IParishService _parishService;

        public HomeController(IParishService parishService)
        {
            _parishService = parishService;
        }
        
        public IActionResult Index()
        {
            var model = _parishService
                .GetAll()
                .Select(x => new ParishListItemViewModel()
                {
                    ParishId = x.Id,
                    ParishName = x.ParishName
                });

            return View(model);
        }
    }
}
