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
            PostCategories = new HashSet<PostCategory>();
            Reactions = new HashSet<Reaction>();
            Reports = new HashSet<Report>();
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
        public int Status { get; set; }
        [Column("scope")]
        public int Scope { get; set; }

        [Column("note")]
        public string Note { get; set; } = null;
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
        public virtual Artist Artist { get; set; } = null!;
        [InverseProperty("Post")]
        public virtual ICollection<Bookmark> Bookmarks { get; set; }
        [InverseProperty("Post")]
        public virtual ICollection<Image> Images { get; set; }
        [InverseProperty("Post")]
        public virtual ICollection<PostCategory> PostCategories { get; set; }
        [InverseProperty("Post")]
        public virtual ICollection<Reaction> Reactions { get; set; }
        
        [InverseProperty("Post")]
        public virtual ICollection<Report> Reports { get; set; }
    }
}
