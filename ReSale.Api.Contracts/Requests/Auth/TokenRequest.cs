using System.ComponentModel.DataAnnotations;

namespace ReSale.Api.Contracts.Requests.Auth;

public record TokenRequest
{
    [Required]
    [EmailAddress]
    [MinLength(5)]
    public string Email { get; set; } = null!;

    [Required]
    [MinLength(8)]
    public string Password { get; set; } = null!;
};