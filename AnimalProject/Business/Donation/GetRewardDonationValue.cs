using AnimalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalProject.Business.Donation
{
    public class GetRewardDonationValue
    {
        public string GetValue(Medicine medicine) {

            var medicineName = medicine.Name;

            return medicineName;
        
        }
    }
}
