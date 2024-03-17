using ArtHubBO.Entities;
using System.ComponentModel.DataAnnotations;

namespace ArtHubBO.DTO;

public class PostUpdateDto
{
    [Required(ErrorMessage = "Post id is required!")]
    public int PostId { get; set; }
    [Required(ErrorMessage = "Title is required!")]
    public string Title { get; set; } = null!;
    [Required(ErrorMessage = "Description is required!")]
    public string Description { get; set; } = null!;
    [Required(ErrorMessage = "Scope is required!")]
    public int Scope { get; set; }
    [Required(ErrorMessage = "ArtistEmail is required!")]
    public string ArtistEmail { get; set; } = null!;
    [Required(ErrorMessage = "Images is required!")]
    public virtual ICollection<Image> Images { get; set; }
    [Required(ErrorMessage = "Post categories is required!")]
    public virtual ICollection<PostCategory> PostCategories { get; set; }   
}
