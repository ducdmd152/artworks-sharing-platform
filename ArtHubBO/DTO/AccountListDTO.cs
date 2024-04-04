using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtHubBO.DTO
{

    public class AccountListDTO
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public int Status { get; set; }
        public bool Enabled { get; set; }
        public string? Avatar { get; set; }

        public string AccountCreateDate { get; set; }
        public string AccountUpdateDate { get; set; }

        public int TotalPages { get; set; }
        public int TotalItems { get; set; }
    }
}
