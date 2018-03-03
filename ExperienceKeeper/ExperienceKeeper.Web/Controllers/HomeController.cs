using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ExperienceKeeper.Entity.Models.View;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ExperienceKeeper.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration Configuration;
        private readonly IHostingEnvironment HostingEnvironment;

        public HomeController(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            HostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            ViewBag.Test = Configuration.GetConnectionString("DataAccessPostgreSqlProvider");
            ViewBag.WebRootPath = HostingEnvironment.WebRootPath;
            ViewBag.ContentRootPath = HostingEnvironment.ContentRootPath;
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
