using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtHubBO.Entities
{
    [Table("role")]
    public partial class Role : BaseEntity
    {
        public Role()
        {
            Accounts = new HashSet<Account>();
        }

        [Key]
        [Column("role_id")]
        public int RoleId { get; set; }
        [Column("role_name")]
        [StringLength(256)]
        public string RoleName { get; set; } = null!;
        [InverseProperty("Role")]
        public virtual ICollection<Account> Accounts { get; set; }
    }
}
