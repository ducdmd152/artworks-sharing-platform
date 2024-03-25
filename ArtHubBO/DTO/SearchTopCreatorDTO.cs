using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtHubBO.DTO
{
    
    public class SearchTopCreatorDTO
    {
       // public string StartDate { get; set; }
       public string Email { get; set; }
        public string  Date { get; set; }
        public int PageNumber { get; set; }
        [DefaultValue(10)]
        public int PageSize { get; set; }
    }
}


