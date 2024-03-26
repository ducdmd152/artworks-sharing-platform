using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtHubBO.DTO;

public class TopCreatorDTO
{
    public string CreatorEmail { get; set; }

    public string CreatorName { get; set; }

    public int TotalSubscribe { get; set; }

    public double Fee { get; set; }

    public int TotalLove { get; set; }
   

    public double  Revenue { get; set; }

    public int TotalPages { get; set; }
    public int TotalItems { get; set; }
}
