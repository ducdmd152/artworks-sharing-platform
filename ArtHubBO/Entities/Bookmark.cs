using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ArtHubBO.Entities
{
    [Table("bookmark")]
    public partial class Bookmark : BaseEntity
    {
        [Key]
        [Column("bookmark_id")]
        public int BookmarkId { get; set; }
        [Column("delete_flag")]
        public bool DeleteFlag { get; set; }
        [Column("post_id")]
        public int PostId { get; set; }
        [Column("account_email")]
        [StringLength(256)]
        [Unicode(false)]
        public string AccountEmail { get; set; } = null!;

        [ForeignKey("AccountEmail")]
        [InverseProperty("Bookmarks")]
        public virtual Account AccountEmailNavigation { get; set; } = null!;
        [ForeignKey("PostId")]
        [InverseProperty("Bookmarks")]
        public virtual Post Post { get; set; } = null!;
    }
}
