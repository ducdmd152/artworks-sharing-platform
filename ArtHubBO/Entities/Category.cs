using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ArtHubBO.Entities
{
    [Table("category")]
    [Index("CategoryName", Name = "category_unique", IsUnique = true)]
    public partial class Category : BaseEntity
    {
        [Key]
        [Column("category_id")]
        public int CategoryId { get; set; }
        [Column("category_name")]
        [StringLength(100)]
        [Unicode(false)]
        public string CategoryName { get; set; } = null!;
        [Column("description")]
        [StringLength(512)]
        public string Description { get; set; } = null!;
    }
}
