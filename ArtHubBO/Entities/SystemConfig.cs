﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtHubBO.Entities
{
    [Table("system_config")]
    public partial class SystemConfig : BaseEntity
    {
        [Key]
        [Column("config_id")]
        public int ConfigId { get; set; }
        [Column("commision_rate")]
        public double CommisionRate { get; set; }
    }
}
