using System.ComponentModel.DataAnnotations;

namespace OceanCareChat.Dtos.Reports
{
    public class RegisterReportsDTO
    {
        [Required]
        public string TrashType { get; set; } = string.Empty;
        [Required]
        public string TrashLocation { get; set; } = string.Empty;
        [Required]
        public string TrashDescription { get; set; } = string.Empty;
        [Required]
        public int OceanUserId { get; set; } 
    }
}
