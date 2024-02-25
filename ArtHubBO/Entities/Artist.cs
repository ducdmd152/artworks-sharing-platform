using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ArtHubBO.Entities
{
    [Table("artist")]
    public partial class Artist : BaseEntity
    {
        public Artist()
        {
            Fees = new HashSet<Fee>();
            Posts = new HashSet<Post>();
            Subscribers = new HashSet<Subscriber>();
        }

        [Key]
        [Column("email")]
        [StringLength(256)]
        [Unicode(false)]
        public string Email { get; set; } = null!;
        [Column("artist_name")]
        [StringLength(256)]
        public string ArtistName { get; set; } = null!;
        [Column("bio")]
        [StringLength(512)]
        public string? Bio { get; set; }
        [Column("total_subscribe")]
        public int TotalSubscribe { get; set; }

        [ForeignKey("Email")]
        [InverseProperty("Artist")]
        public virtual Account EmailNavigation { get; set; } = null!;
        [InverseProperty("ArtistEmailNavigation")]
        public virtual ICollection<Fee> Fees { get; set; }
        [InverseProperty("ArtistEmailNavigation")]
        public virtual ICollection<Post> Posts { get; set; }
        [InverseProperty("EmailArtistNavigation")]
        public virtual ICollection<Subscriber> Subscribers { get; set; }
    }
}
