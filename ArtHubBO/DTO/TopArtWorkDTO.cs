using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtHubBO.DTO
{

    public class TopArtWorkDTO
    {
        public string PostTitle { get; set; }
        public string CreatorName { get; set; }
        public int LoveCount { get; set; }
        public int SaveCount { get; set; }
        public int ViewCount { get; set; }

        public int TotalPages { get; set; }
        public int TotalItems { get; set; }
    }
}
