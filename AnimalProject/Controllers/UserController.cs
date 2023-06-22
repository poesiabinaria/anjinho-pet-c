using AnimalProject.Data;
using AnimalProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AnimalProject.Controllers
{
    [ApiController]
    [Route("v1/user/shelters")]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<User>>> GetShelters(
            [FromServices] AppDbContext context
        )
        {
            var shelters = await context.Users.Where(User => User.Role == "shelter").ToListAsync();

            return shelters;
        }

        
    }
}
