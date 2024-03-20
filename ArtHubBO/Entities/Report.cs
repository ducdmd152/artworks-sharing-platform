using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ArtHubBO.Entities
{
    [Table("report")]
    public partial class Report : BaseEntity
    {
        [Key]
        [Column("report_id")]
        public int ReportId { get; set; }
        [Column("reason")]
        [StringLength(256)]
        public string? Reason { get; set; }
        [Column("status")]
        public int Status { get; set; }
        [Column("reporter_email")]
        [StringLength(256)]
        [Unicode(false)]
        public string ReporterEmail { get; set; } = null!;
        [Column("post_id")]
        public int PostId { get; set; }

        [ForeignKey("PostId")]
        [InverseProperty("Reports")]
        public virtual Post Post { get; set; } = null!;
        [ForeignKey("ReporterEmail")]
        [InverseProperty("Reports")]
        public virtual Account ReporterEmailNavigation { get; set; } = null!;
    }
}
