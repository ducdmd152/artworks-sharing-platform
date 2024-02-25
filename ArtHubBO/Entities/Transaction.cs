using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ArtHubBO.Entities
{
    [Table("transaction")]
    public partial class Transaction : BaseEntity
    {
        [Key]
        [Column("transaction_id")]
        public int TransactionId { get; set; }
        [Column("amount")]
        public double Amount { get; set; }
        [Column("status")]
        [StringLength(100)]
        [Unicode(false)]
        public string Status { get; set; } = null!;
        [Column("type")]
        [StringLength(100)]
        [Unicode(false)]
        public string Type { get; set; } = null!;
        [Column("fee_id")]
        public int FeeId { get; set; }
        [Column("subscriber_id")]
        public int SubscriberId { get; set; }

        [ForeignKey("FeeId")]
        [InverseProperty("Transactions")]
        public virtual Fee Fee { get; set; } = null!;
        [ForeignKey("SubscriberId")]
        [InverseProperty("Transactions")]
        public virtual Subscriber Subscriber { get; set; } = null!;
    }
}
