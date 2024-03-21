using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtHubBO.DTO
{
    public class SuggestPostDTO
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public int Scope { get; set; }
        public int TotalReact { get; set; }
        public int TotalView { get; set; }
        public int TotalBookmark { get; set; }
        public string ArtistEmail { get; set; }
        public string ArtistName { get; set; }
    }
}
