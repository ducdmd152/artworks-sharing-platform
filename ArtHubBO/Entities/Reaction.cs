using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ArtHubBO.Entities
{
    [Table("reaction")]
    public partial class Reaction : BaseEntity
    {
        [Key]
        [Column("reaction_id")]
        public int ReactionId { get; set; }
        [Column("post_id")]
        public int PostId { get; set; }
        [Column("account_email")]
        [StringLength(256)]
        [Unicode(false)]
        public string AccountEmail { get; set; } = null!;

        [ForeignKey("AccountEmail")]
        [InverseProperty("Reactions")]
        public virtual Account AccountEmailNavigation { get; set; } = null!;
        [ForeignKey("PostId")]
        [InverseProperty("Reactions")]
        public virtual Post Post { get; set; } = null!;
    }
}
