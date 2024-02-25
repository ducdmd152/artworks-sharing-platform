using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ArtHubBO.Entities
{
    [Table("image")]
    public partial class Image : BaseEntity
    {
        [Key]
        [Column("image_id")]
        public int ImageId { get; set; }
        [Column("type")]
        [StringLength(100)]
        [Unicode(false)]
        public string Type { get; set; } = null!;
        [Column("image_url")]
        [StringLength(256)]
        [Unicode(false)]
        public string ImageUrl { get; set; } = null!;
        [Column("delete_flag")]
        public bool DeleteFlag { get; set; }
        [Column("post_id")]
        public int PostId { get; set; }

        [ForeignKey("PostId")]
        [InverseProperty("Images")]
        public virtual Post Post { get; set; } = null!;
    }
}
