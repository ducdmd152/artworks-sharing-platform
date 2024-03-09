using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtHubBO.Entities
{
    [Table("post_category")]
    public partial class PostCategory : BaseEntity
    {
        [Key]
        [Column("category_id")]
        public int CategoryId { get; set; }
        [Key]
        [Column("post_id")]
        public int PostId { get; set; }

        [ForeignKey("CategoryId")]
        [InverseProperty("PostCategories")]
        public virtual Category Category { get; set; } = null!;
        [ForeignKey("PostId")]
        [InverseProperty("PostCategories")]
        public virtual Post Post { get; set; } = null!;
    }
}
