using AnimalProject.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalProject.Models
{
    public class Medicine
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public string Form { get; set; }
        public string Dose { get; set; }
        public string AvailableQty { get; set; }
        public string ImagePath { get; set; }
        public DateTime ExpirationDate { get; set; }

        public int DonorUserId { get; set; }
        public User DonorUser { get; set; }
    }
}
