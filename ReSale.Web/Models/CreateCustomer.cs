using System.ComponentModel.DataAnnotations;

namespace ReSale.Web.Models;

public class CreateCustomer
{
    [Required]
    public string FirstName { get; set; } = string.Empty;
    
    [Required]
    public string LastName { get; set; } = string.Empty;
    
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    
    [Required]
    public string Street { get; set; } = string.Empty;
    
    [Required]
    public string City { get; set; } = string.Empty;
    
    [Required]
    public string ZipCode { get; set; } = string.Empty;
    
    [Required]
    public string Country { get; set; } = string.Empty;
    
    public string State { get; set; } = string.Empty;
}