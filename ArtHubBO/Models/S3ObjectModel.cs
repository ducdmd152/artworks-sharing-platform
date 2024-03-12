using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtHubBO.Models;

public class S3ObjectModel
{
    public string Name { get; set; } = null!;
    public MemoryStream InputStream { get; set; } = null!;

    public string BucketName { get; set; } = null!;
}
