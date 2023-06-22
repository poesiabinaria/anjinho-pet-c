using AnimalProject.Data;
using AnimalProject.Models;
using AnimalProject.ViewModels;
using AnimalProject.Business.Donation;
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
    [Route("v1/donation")]
    public class DonationController : ControllerBase
    {
        [HttpPost("new")]
        [Authorize]
        public async Task<ActionResult<List<Medicine>>> NewDonation(
            [FromServices] AppDbContext context,
            [FromBody] DonationViewModel model
        )
        {
            int usuarioid = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var donation = new Donation
            {
                DonorUserId = usuarioid,
                DoneeUserId = model.DoneeUserId,
                MedicineId = model.MedicineId
            };

            try
            {
                await context.Donations.AddAsync(donation);
                await context.SaveChangesAsync();
                return Ok();

            }
            catch (Exception e) { return BadRequest(); }

        }

        [HttpGet("received/{userId}")]
        [Authorize]
        public async Task<IList<DonationDTO>> GetDonationsReceivedByUser(
            [FromServices] AppDbContext context,
            [FromRoute] int userId
        )
        {
            var donationsReceived = await context.Users.
                Where(u => u.Id == userId).
                SelectMany(u => u.DonationsReceived).
                Select(m => m.Medicine).
                Select(m => new DonationDTO()
                {
                    Category = m.Category,
                    Name = m.Name,
                    Form = m.Form,
                    Dose = m.Dose,
                    AvailableQty = m.AvailableQty,
                    ImagePath = m.ImagePath,
                    ExpirationDate = m.ExpirationDate
                }).
                ToListAsync();

            return donationsReceived;

        }

        [HttpGet("made/{userId}")]
        [Authorize]
        public async Task<IList<DonationDTO>> GetDonationsMadeByUser(
            [FromServices] AppDbContext context,
            [FromRoute] int userId
        )
        {
            var donationsMade = await context.Users.
                Where(u => u.Id == userId).
                SelectMany(u => u.DonationsMade).
                Select(m => m.Medicine).
                Select(m => new DonationDTO()
                {
                    Category = m.Category,
                    Name = m.Name,
                    Form = m.Form,
                    Dose = m.Dose,
                    AvailableQty = m.AvailableQty,
                    ImagePath = m.ImagePath,
                    ExpirationDate = m.ExpirationDate
                }).
                ToListAsync();

            return donationsMade;

        }

        [HttpPut("update/{donationId}")]
        [Authorize]
        public async Task<ActionResult<List<Medicine>>> UpdateDonation(
            [FromServices] AppDbContext context,
            [FromRoute] int donationId,
            [FromBody] DonationViewModel model
        )
        {
            var donation = await context.Donations.FirstOrDefaultAsync(d => d.Id == donationId);

            if (donation == null)
            {
                NotFound();
            }

            donation.DoneeUserId = model.DoneeUserId;
            donation.Status = model.Status;

            try
            {
                context.Donations.Update(donation);
                await context.SaveChangesAsync();
                return Ok();

            }
            catch (Exception e) { return BadRequest(); }

        }

        [HttpDelete("delete/{donationId}")]
        [Authorize]
        public async Task<ActionResult<List<Medicine>>> DeleteDonation(
            [FromServices] AppDbContext context,
            [FromRoute] int donationId
        )
        {
            var donation = await context.Donations.FirstOrDefaultAsync(d => d.Id == donationId);

            if (donation == null)
            {
                NotFound();
            }

            try
            {
                context.Donations.Remove(donation);
                await context.SaveChangesAsync();
                return Ok();

            }
            catch (Exception e) { return BadRequest(); }

        }

        [HttpGet("reward")]
        [Authorize]
        public async Task<string> GetRewardValue(
            [FromServices] AppDbContext context,
            [FromBody] DonationViewModel model
        )
        {
            var medicine = await context.Medicines.FirstOrDefaultAsync(m => m.Id == model.MedicineId);

           

            var test = new GetRewardDonationValue().GetValue(medicine);

            return test;

        }

    }
}

