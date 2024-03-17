using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ArtHubBO.Entities
{
    [Table("subscriber")]
    public partial class Subscriber : BaseEntity
    {
        public Subscriber()
        {
            Transactions = new HashSet<Transaction>();
        }

        [Key]
        [Column("subscriber_id")]
        public int SubscriberId { get; set; }
        [Column("email_user")]
        [StringLength(256)]
        [Unicode(false)]
        public string EmailUser { get; set; } = null!;
        [Column("email_artist")]
        [StringLength(256)]
        [Unicode(false)]
        public string EmailArtist { get; set; } = null!;
        [Column("status")]
        public int Status { get; set; }
        [Column("expired_date", TypeName = "datetime")]
        public DateTime ExpiredDate { get; set; }

        [ForeignKey("EmailArtist")]
        [InverseProperty("Subscribers")]
        public virtual Artist EmailArtistNavigation { get; set; } = null!;
        [ForeignKey("EmailUser")]
        [InverseProperty("Subscribers")]
        public virtual Account EmailUserNavigation { get; set; } = null!;
        [InverseProperty("Subscriber")]
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
