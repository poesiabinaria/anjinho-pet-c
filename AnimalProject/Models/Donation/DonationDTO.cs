using AnimalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalProject.ViewModels
{
    public class DonationDTO
    {
        public string Category { get; set; }
        public string Name { get; set; }
        public string Form { get; set; }
        public string Dose { get; set; }
        public string AvailableQty { get; set; }
        public string ImagePath { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
