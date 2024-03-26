using System.ComponentModel.DataAnnotations;

namespace ArtHubBO.DTO;

public class AccountRegisterDto
{
    [Required(ErrorMessage = "Gender is required!")]
    public string Gender { get; set; } = null!;
    [Required(ErrorMessage = "Type of register is required!")]
    public bool IsRegisterCreator { get; set; } = false;
    [Required(ErrorMessage = "First name is required!")]
    public string FirstName { get; set; } = null!;
    [Required(ErrorMessage = "Last name is required!")]
    public string LastName { get; set; } = null!;
    [Required(ErrorMessage = "Account email is required!")]
    public string Email { get; set; } = null!;
    [Required(ErrorMessage = "Artist name is required!")]
    public string ArtistName { get; set; } = null!;
    [Required(ErrorMessage = "Password is required.")]
    public string Password { get; set; } = null!;
    [Required(ErrorMessage = "Confirmation Password is required.")]
    [Compare("Password", ErrorMessage = "Password and Confirmation Password must match.")]
    public string ConfirmPassword { get; set; } = null!;
}
