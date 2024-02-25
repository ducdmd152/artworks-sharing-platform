using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ArtHubBO.Entities
{
    [Table("account")]
    public partial class Account : BaseEntity
    {
        public Account()
        {
            Bookmarks = new HashSet<Bookmark>();
            Reactions = new HashSet<Reaction>();
            Subscribers = new HashSet<Subscriber>();
        }

        [Key]
        [Column("email")]
        [StringLength(256)]
        [Unicode(false)]
        public string Email { get; set; } = null!;
        [Column("password")]
        [StringLength(100)]
        [Unicode(false)]
        public string Password { get; set; } = null!;
        [Column("first_name")]
        [StringLength(256)]
        public string FirstName { get; set; } = null!;
        [Column("last_name")]
        [StringLength(100)]
        public string LastName { get; set; } = null!;
        [Column("gender")]
        [StringLength(100)]
        [Unicode(false)]
        public string Gender { get; set; } = null!;
        [Column("status")]
        [StringLength(100)]
        [Unicode(false)]
        public string Status { get; set; } = null!;
        [Column("enabled")]
        public bool Enabled { get; set; }
        [Column("avatar")]
        [StringLength(256)]
        [Unicode(false)]
        public string? Avatar { get; set; }
        [Column("role_id")]
        public int RoleId { get; set; }
        [InverseProperty("EmailNavigation")]
        public virtual Artist? Artist { get; set; }
        [InverseProperty("AccountEmailNavigation")]
        public virtual ICollection<Bookmark> Bookmarks { get; set; }
        [InverseProperty("AccountEmailNavigation")]
        public virtual ICollection<Reaction> Reactions { get; set; }
        [InverseProperty("EmailUserNavigation")]
        public virtual ICollection<Subscriber> Subscribers { get; set; }
    }
}
