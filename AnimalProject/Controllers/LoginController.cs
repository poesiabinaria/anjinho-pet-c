using AnimalProject.Data;
using AnimalProject.Models;                                                                           
using AnimalProject.Services;
using AnimalProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalProject.Controllers
{
    [ApiController]
    [Route("v1/user")]
    public class LoginController : ControllerBase
    {

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> AuthenticateAsync(
            [FromServices] AppDbContext context, 
            [FromBody] User model) {
            
            var user = await context.Users.FirstOrDefaultAsync(user => user.Username == model.Username);
            
            if ( user == null ) { return NotFound(new { message = "Usuário ou senha inválidos" }); }
             
        
            var token = TokenService.GenerateToken(user);
            return Ok(token);



        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterAsync(
            [FromServices] AppDbContext context, 
            [FromBody] CreateNewUserViewModel model) {

            if (!ModelState.IsValid) return BadRequest();

            var existentUser = await context.Users.FirstOrDefaultAsync(user => 
            user.Username == model.Username ||
            user.EmailAdress == model.EmailAdress
            );

            if (existentUser == null) {
                var user = new User()
                {
                    Username = model.Username,
                    EmailAdress = model.EmailAdress,
                    Password = model.Password,
                    FirstName = model.FirstName,
                    Role = model.Role,
                };

                try
                {
                    await context.Users.AddAsync(user);
                    await context.SaveChangesAsync();
                    return Created($"v1/users/{user.Id}", user);
                }
                catch (Exception e) { return BadRequest(); }
            }

            return BadRequest(new { message = "Nome de usuário ou e-mail já castrados" });
        }
    }
}
