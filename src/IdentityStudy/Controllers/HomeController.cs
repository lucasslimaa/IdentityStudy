using IdentityStudy.Extensions;
using IdentityStudy.Models;
using KissLog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace IdentityStudy.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger _logger;

        public HomeController(ILogger logger)
        {
            _logger = logger;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            _logger.Trace("The user has reached the index");
            return View();
        }


        public IActionResult Privacy()
        {
            try
            {
                throw new Exception("Something went terribly wrong!");
            }
            catch (Exception e)
            {

                _logger.Error(e);
                throw;
            }
            
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
        [ClaimsAuthorize("Products", "Read")]
        public IActionResult ClaimsCustom()
        {
            return View("Secret");
        }

        [Route("erro/{id:length(3,3)}")]
        public IActionResult Error(int id)
        {
            var modelErro = new ErrorViewModel();

            if (id == 500)
            {
                modelErro.Message = "An Error has occured! try again later";
                modelErro.Title = "An Error has occured!";
                modelErro.ErroCode = id;
            }
            else if(id == 404)
            {
                modelErro.Message = "The page you requested was not found!";
                modelErro.Title = "Ops! Page not found.";
                modelErro.ErroCode = id;
            }
            else if (id == 403)
            {
                modelErro.Message = "You dont have access to this page!";
                modelErro.Title = "Access denied.";
                modelErro.ErroCode = id;
            }
            else
            {
                return StatusCode(404);
            }


            return View("Error", modelErro);
        }
    }
}
