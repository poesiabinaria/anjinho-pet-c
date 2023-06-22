using AnimalProject.Data;
using AnimalProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalProject.Controllers
{
    [ApiController]
    [Route("v1")]
    public class HomeController : ControllerBase
    {

        [HttpGet]
        [Route("anonymous")]
        [AllowAnonymous]
        public string Anonymous() => "Público";

        [HttpGet]
        [Route("authenticated")]
        [Authorize]
        public string Autenticated() => $"Olá, {User.Identity.Name}!";

        [HttpGet]
        [Route("tutor")]
        [Authorize(Roles = "tutor")]
        public string Tutor() => "Sou um tutor";

        [HttpGet]
        [Route("shelter")]
        [Authorize(Roles = "abrigo")]
        public string Shelter() => "Sou um abrigo";

        [HttpGet]
        [Route("veterinary")]
        [Authorize(Roles = "veterinary")]
        public string Veterinary() => "Sou um veterinário";

        
    }
}
