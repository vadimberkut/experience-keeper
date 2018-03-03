using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExperienceKeeper.Controllers
{
    /// <summary>
    /// Base controller for authenticated user
    /// </summary>
    [Authorize]
    public class MainController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}