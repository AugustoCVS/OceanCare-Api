using System.ComponentModel.DataAnnotations;

namespace OceanCareChat.Dtos.User
{
    public class UpdateUserDTO
    {
 
            public string Name { get; set; } = string.Empty;
            [EmailAddress]
            public string Email { get; set; } = string.Empty;
        

    }
}
