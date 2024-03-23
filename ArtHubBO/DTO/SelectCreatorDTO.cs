using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtHubBO.DTO
{
    public class SelectCreatorDTO
    {
        public string ArtistEmail { get; set; }
        public string ArtistBio { get; set; }
        public string ArtistName { get; set; }
        public string ArtistAvatar { get; set; }
        public int TotalPages { get; set; }
        public int TotalItems { get; set; }
    }
}
