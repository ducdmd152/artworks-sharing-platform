using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ArtHubBO.Entities
{
    [Table("post")]
    public partial class Post : BaseEntity
    {
        public Post()
        {
            Bookmarks = new HashSet<Bookmark>();
            Images = new HashSet<Image>();
            Reactions = new HashSet<Reaction>();
        }

        [Key]
        [Column("post_id")]
        public int PostId { get; set; }
        [Column("title")]
        [StringLength(256)]
        public string Title { get; set; } = null!;
        [Column("description")]
        [StringLength(512)]
        public string Description { get; set; } = null!;
        [Column("status")]
        [StringLength(100)]
        [Unicode(false)]
        public string Status { get; set; } = null!;
        [Column("scope")]
        [StringLength(100)]
        [Unicode(false)]
        public string Scope { get; set; } = null!;
        [Column("total_react")]
        public int TotalReact { get; set; }
        [Column("total_view")]
        public int TotalView { get; set; }
        [Column("total_bookmark")]
        public int TotalBookmark { get; set; }
        [Column("artist_email")]
        [StringLength(256)]
        [Unicode(false)]
        public string ArtistEmail { get; set; } = null!;

        [ForeignKey("ArtistEmail")]
        [InverseProperty("Posts")]
        public virtual Artist ArtistEmailNavigation { get; set; } = null!;
        [InverseProperty("Post")]
        public virtual ICollection<Bookmark> Bookmarks { get; set; }
        [InverseProperty("Post")]
        public virtual ICollection<Image> Images { get; set; }
        [InverseProperty("Post")]
        public virtual ICollection<Reaction> Reactions { get; set; }
    }
}
