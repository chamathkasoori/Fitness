using System.ComponentModel.DataAnnotations;

namespace Fitness.Dtos;
public class ForgotPasswordDto
{
    [Required]
    public string Email { get; set; } = string.Empty;
}
