using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalProject.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string EmailAdress { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public string Role { get; set; }

        public IEnumerable<Medicine> Medicines { get; set; }
        public IEnumerable<Donation> DonationsMade { get; set; }
        public IEnumerable<Donation> DonationsReceived { get; set; }
        


    }
}
