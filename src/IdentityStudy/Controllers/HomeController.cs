using IdentityStudy.Extensions;
using IdentityStudy.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using static IdentityStudy.Extensions.CustomAuth;

namespace IdentityStudy.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
       
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        //ROLES
        [Authorize(Roles = "Admin")]
        public IActionResult Secret()
        {
            return View();
        }

        //SIMPLE CLAIM
        [Authorize(Policy = "CanDelete")]
        public IActionResult SecretClaim()
        {
            return View("Secret");
        }

        //COMPOSED CLAIM VALUES
        [Authorize(Policy = "CanRead")]
        public IActionResult SecretClaimRead()
        {
            return View("Secret");
        }

        //CUSTOMIZED CLAIMS
        //VIEW THE DATA TABLE AND THE CUSTOMAUTH CLASS TO UNDERSTAND THE IMPLEMENTATION
        [ClaimsAuthorize("Products", "Read") ]
        public IActionResult ClaimsCustom()
        {
            return View("Secret");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
