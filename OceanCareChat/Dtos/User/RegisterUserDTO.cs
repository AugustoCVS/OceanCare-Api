using System.ComponentModel.DataAnnotations;

namespace OceanCareChat.Dtos.User
{
    public class RegisterUserDTO
    {
  
            [Required]
            public string Name { get; set; } = string.Empty;
            [Required]
            [EmailAddress]
            public string Email { get; set; } = string.Empty;
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; } = string.Empty;
        
    }
}
