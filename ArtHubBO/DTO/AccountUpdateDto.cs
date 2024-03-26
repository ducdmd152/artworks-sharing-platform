using System.ComponentModel.DataAnnotations;

namespace ArtHubBO.DTO;

public class AccountUpdateDto
{
    [Required(ErrorMessage = "Account email is required!")]
    public string Email { get; set; } = null!;
    [Required(ErrorMessage = "First name is required!")]
    public string FirstName { get; set; } = null!;
    [Required(ErrorMessage = "Last name is required!")]
    public string LastName { get; set; } = null!;
    [Required(ErrorMessage = "Gender is required!")]
    public string Gender { get; set; } = null!;
    public string? Avatar { get; set; }
    [Required(ErrorMessage = "Artist name is required!")]
    public string ArtistName { get; set; } = null!;    
    public string? Bio { get; set; }
}
