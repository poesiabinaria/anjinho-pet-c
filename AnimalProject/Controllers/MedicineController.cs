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
    [Route("v1/medicine")]
    public class MedicineController : ControllerBase
    {
        [HttpGet("all")]
        public async Task<ActionResult<List<Medicine>>> GetAll( 
            [FromServices] AppDbContext context 
        )
        { 
            return await context.Medicines.ToListAsync(); 
        }

        [HttpGet("user/{userId}")]
        [Authorize]
        public async Task<ActionResult<List<Medicine>>> GetByUser(
            [FromServices] AppDbContext context,
            [FromRoute] int userId
            )
        {

            var medicines = await context.
                    Medicines.
                    Where(medicine => medicine.DonorUserId == userId).
                    ToListAsync();

            return medicines;

        }

        [HttpPost("add")]
        [Authorize]
        public async Task<IActionResult> AddMedicine(
            [FromServices]AppDbContext context, 
            [FromBody] Medicine model) {

            int usuarioId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var medicine = new Medicine {
                Name = model.Name,
                Category = model.Category,
                Form = model.Form,
                Dose = model.Dose,
                AvailableQty = model.AvailableQty,
                ImagePath = model.ImagePath,
                ExpirationDate = model.ExpirationDate,
                DonorUserId = usuarioId,
            };

            try
            {
                await context.Medicines.AddAsync(medicine);
                await context.SaveChangesAsync();
                return Created($"v1/medicine/{medicine.Id}", medicine);
            }
            catch (Exception e) { return BadRequest(); }
        
        }

        [HttpPut("update/{medicineId}")]
        [Authorize]
        public async Task<IActionResult> UpdateMedicine(
            [FromServices] AppDbContext context,
            [FromBody] Medicine model,
            [FromRoute] int medicineId)
        {

            var medicine = await context.Medicines.FirstOrDefaultAsync(medicine => medicine.Id == medicineId);

            if (medicine == null) {
                return NotFound();
            }

            medicine.Name = model.Name;
            medicine.Category = model.Category;
            medicine.Form = model.Form;
            medicine.Dose = model.Dose;
            medicine.AvailableQty = model.AvailableQty;
            medicine.ImagePath = model.ImagePath;
            medicine.ExpirationDate = model.ExpirationDate;

            try
            {
                context.Medicines.Update(medicine);
                await context.SaveChangesAsync();
                return Ok(medicine);
            }
            catch (Exception e) { return BadRequest(); }

        }

        [HttpDelete("delete/{medicineId}")]
        [Authorize]
        public async Task<IActionResult> DeleteMedicine(
            [FromServices] AppDbContext context,
            [FromRoute] int medicineId)
        {

            var medicine = await context.Medicines.FirstOrDefaultAsync(medicine => medicine.Id == medicineId);

            if (medicine == null)
            {
                return NotFound();
            }

            try
            {
                context.Medicines.Remove(medicine);
                await context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception e) { return BadRequest(); }
        }
    }
}
