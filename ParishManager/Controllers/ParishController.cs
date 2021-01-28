using Microsoft.AspNetCore.Mvc;
using ParishManager.Models.Parish;
using ParishManager.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParishManager.Controllers
{
    public class ParishController : Controller
    {
        private readonly IParishService _parishService;

        public ParishController(IParishService parishService)
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
