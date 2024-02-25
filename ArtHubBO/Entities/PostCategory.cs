using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ArtHubBO.Entities
{
    [Keyless]
    [Table("post_category")]
    public partial class PostCategory : BaseEntity
    {
        [Column("category_id")]
        public int CategoryId { get; set; }
        [Column("post_id")]
        public int PostId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; } = null!;
        [ForeignKey("PostId")]
        public virtual Post Post { get; set; } = null!;
    }
}
