using Microsoft.AspNetCore.Mvc;
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

        public ActionResult Index()
        {
            var x = _parishService.GetAllParishes();

            return View();
        }
    }
}
