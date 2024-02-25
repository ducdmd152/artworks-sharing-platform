﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtHubBO.Entities
{
    public abstract class BaseEntity
    {
        [Column("created_date", TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }
        [Column("updated_date", TypeName = "datetime")]
        public DateTime UpdatedDate { get; set; }
    }
}
