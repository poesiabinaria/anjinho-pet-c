using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalProject.ViewModels
{
    public class DonationViewModel
    {
        public int MedicineId { get; set; }
        public int DoneeUserId { get; set; }
        public int Status { get; set; }
    }
}
