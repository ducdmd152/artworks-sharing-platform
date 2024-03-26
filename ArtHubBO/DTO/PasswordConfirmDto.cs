using System.ComponentModel.DataAnnotations;

namespace ArtHubBO.DTO;

public class PasswordConfirmDto
{
    [Required(ErrorMessage = "Old password is required.")]
    public string OldPassword { get; set; } = null!;
    [Required(ErrorMessage = "New password is required.")]
    public string NewPassword { get; set; } = null!;

    [Required(ErrorMessage = "Confirmation Password is required.")]
    [Compare("NewPassword", ErrorMessage = "New Password and Confirmation Password must match.")]
    public string ConfirmPassword { get; set; } = null!;
}
