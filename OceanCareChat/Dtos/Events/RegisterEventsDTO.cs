using System.ComponentModel.DataAnnotations;

namespace OceanCareChat.Dtos.Events
{
    public class RegisterEventsDTO
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Date { get; set; } = string.Empty;

        [Required]
        public string Location { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;
    }
}
