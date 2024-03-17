using System.ComponentModel.DataAnnotations.Schema;
namespace ArtHubBO.Entities;

public abstract class BaseEntity
{
    [Column("created_date", TypeName = "datetime")]
    public DateTime CreatedDate { get; set; }
    [Column("updated_date", TypeName = "datetime")]
    public DateTime UpdatedDate { get; set; }
}