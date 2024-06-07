﻿using System.ComponentModel.DataAnnotations;

namespace OceanCareChat.Dtos.User
{
    public class LoginDTO
    {
        public class LoginDto
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; } = string.Empty;

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; } = string.Empty;
        }
    }
}
