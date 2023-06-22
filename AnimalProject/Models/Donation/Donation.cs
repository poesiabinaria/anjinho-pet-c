
using AnimalProject.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalProject.Models
{
    public class Donation
    {
        public int Id { get; set; }
        public int Status { get; set; } = (int)DonationStatus.NEW;

        public User DonorUser { get; set; }
        public int DonorUserId { get; set; }

        public User DoneeUser { get; set; }
        public int DoneeUserId { get; set; }

        public Medicine Medicine { get; set; }
        public int MedicineId { get; set; }
    }
}
