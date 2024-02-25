using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ArtHubBO.Entities
{
    [Table("fee")]
    public partial class Fee : BaseEntity
    {
        public Fee()
        {
            Transactions = new HashSet<Transaction>();
        }

        [Key]
        [Column("fee_id")]
        public int FeeId { get; set; }
        [Column("amount")]
        public double Amount { get; set; }
        [Column("artist_email")]
        [StringLength(256)]
        [Unicode(false)]
        public string ArtistEmail { get; set; } = null!;

        [ForeignKey("ArtistEmail")]
        [InverseProperty("Fees")]
        public virtual Artist ArtistEmailNavigation { get; set; } = null!;
        [InverseProperty("Fee")]
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
